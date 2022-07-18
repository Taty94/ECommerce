using AutoMapper;
using AutoMapper.QueryableExtensions;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities;
using Course.ECommerce.Domain.Entities.Order;
using Course.ECommerce.Domain.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Course.ECommerce.Aplication.ServicesImpl
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IGenericRepository<Order> orderRepository;
        private readonly IGenericRepository<Delivery> deliveryRepository;
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ItemOrdered> itemsRepository;
        private readonly IBasketRepository basketRepository;
        private readonly ILocationInfoRepository locationInfoRepository;
        private readonly IMapper mapper;
        private readonly IValidator<CreateOrderDto> validator;

        public OrderApplication(IGenericRepository<Order> orderRepository,
            IGenericRepository<Delivery> deliveryRepository, IGenericRepository<Product> productRepository, IGenericRepository<ItemOrdered> itemsRepository,
            IBasketRepository basketRepository, ILocationInfoRepository locationInfoRepository, IMapper mapper, IValidator<CreateOrderDto> validator)
        {
            this.orderRepository = orderRepository;
            this.deliveryRepository = deliveryRepository;
            this.productRepository = productRepository;
            this.itemsRepository = itemsRepository;
            this.basketRepository = basketRepository;
            this.locationInfoRepository = locationInfoRepository;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<ICollection<DeliveryDto>> GetAllDeliveryByUserAsync(string userEmail)
        {
            var orders = orderRepository.GetQueryable();
            orders = orders.Where(o => o.UserEmail == userEmail);

            var deliveries = orders.Select(o => o.DeliveryMethod).ToList();
            var deliveriesDto = mapper.Map<ICollection<DeliveryDto>>(deliveries);
            return deliveriesDto;

        }

        public async Task<OrderDto> GetAllOrdersByUserAsync(string userEmail, int offset = 0, int limit = 3, string sort = "Date", string order = "asc")
        {
            var orders = orderRepository.GetQueryable();

            if (!string.IsNullOrEmpty(userEmail))
            {
                orders = orders.Where(o => !o.IsDeleted && o.UserEmail.Equals(userEmail));
            }

            var locationData = await locationInfoRepository.GetLocationInfoAsync(userEmail);

            if (locationData == null)
            {
                throw new NotFoundException($"La informacion del user con Email:{userEmail} no existe");
            }

            var total = await orders.CountAsync();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateProjection<Order, OrderDetailedDto>()
                    .ForMember(d => d.Description, opt => opt.MapFrom(org => org.DeliveryMethod.Description))
                    .ForMember(d => d.Price, opt => opt.MapFrom(org => org.DeliveryMethod.Price));
                cfg.CreateMap<ItemOrdered, ItemOrderedDto>();
            });

            var ordersDto = await orders.ProjectTo<OrderDetailedDto>(configuration).ToListAsync();

            //Calculo Total de la Orden
            ordersDto.ForEach(o => o.Total = (o.Subtotal + o.Price));

            var orderDto = new OrderDto();
            orderDto.UserEmail = locationData.Email;
            orderDto.FullName = locationData.FullName;
            orderDto.CityAddress = $"{locationData.City}-{locationData.Adress}";
            orderDto.Phone = locationData.Phone;
            orderDto.Orders = ordersDto;
            orderDto.Total = total;

            if (!string.IsNullOrEmpty(sort))
            {
                //Soportar Campos
                //sort => name or price. Other trwo exception
                switch (sort.ToUpper())
                {
                    case "STATUS":
                        orderDto.Orders = orderDto.Orders.OrderBy(o => o.Status).ToList();
                        break;
                    case "DELIVERY-PRICE":
                        orderDto.Orders = orderDto.Orders.OrderBy(o => o.Price).ToList();
                        break;
                    case "TOTAL":
                        orderDto.Orders = orderDto.Orders.OrderBy(o => o.Total).ToList();
                        break;
                    case "DATE":
                        orderDto.Orders = orderDto.Orders.OrderByDescending(o => o.CreationDate).ToList();
                        break;
                    default:
                        throw new ArgumentException($"The parameter sort {sort} not support");
                }
            }

            orderDto.Orders = orderDto.Orders.Skip(offset).Take(limit).ToList();

            return orderDto;

        }

        public async Task<OrderDetailedDto> GetOrderByIdAsync(Guid id, string userEmail)
        {
            var orders = orderRepository.GetQueryable();
            var ordersByIdForUser = orders.Where(o => !o.IsDeleted && o.UserEmail.Equals(userEmail) && o.Id == id);

            if (ordersByIdForUser.SingleOrDefaultAsync().Result == null)
            {
                throw new NotFoundException($"No existen registro para el user {userEmail} con Orden Id:{id} no existe");
            }

            var configuration = new MapperConfiguration(cfg =>
                            {
                                cfg.CreateProjection<Order, OrderDetailedDto>()
                                    .ForMember(d => d.Description, opt => opt.MapFrom(org => org.DeliveryMethod.Description))
                                    .ForMember(d => d.Price, opt => opt.MapFrom(org => org.DeliveryMethod.Price));
                                cfg.CreateMap<ItemOrdered, ItemOrderedDto>();
                            });

            var resultQuery = await ordersByIdForUser.ProjectTo<OrderDetailedDto>(configuration).SingleOrDefaultAsync();
            //Calculo Total de la Orden
            resultQuery.Total = resultQuery.Subtotal + resultQuery.Price;

            return resultQuery;
        }

        public async Task<OrderDetailedDto> InsertOrderAsync(CreateOrderDto orderDto)
        {
            await validator.ValidateAndThrowAsync(orderDto);

            //1. Obtener carrito
            var basket = await basketRepository.GetBasketAsync(orderDto.BasketId);

            if (basket == null)
            {
                throw new NotFoundException($"Carrito con Id:{orderDto.BasketId} no existe");
            }

            //2. Obtener usuario
            var locationData = await locationInfoRepository.GetLocationInfoAsync(orderDto.UserEmail);
            if (locationData == null)
            {
                throw new NotFoundException($"La informacion del user con Email:{orderDto.UserEmail} no existe");
            }
            //3.obtener productos
            var productsOrdered = new List<ItemOrdered>();
            foreach (var item in basket.Items)
            {
                var product = await productRepository.GetByIdAsync(item.Id);

                if (product == null)
                {
                    throw new NotFoundException($"Producto con Id:{item.Id} no existe");
                }

                var itemOrdered = new ItemOrdered(product.Name, product.Price, item.Quantity);
                var random = new Random();
                itemOrdered.Id = String.Format("{0}", $"I{random.Next(0, 999).ToString()}");
                productsOrdered.Add(itemOrdered);
            }

            //4.Obtener entrega
            var delivery = await deliveryRepository.GetByIdAsync(orderDto.DeliveryId);

            if (delivery == null)
            {
                throw new NotFoundException($"Entrega con Id:{orderDto.DeliveryId} no existe");
            }

            //5.Calcular subtotal
            var subtotal = productsOrdered.Sum(po => po.Price * po.Quantity);

            //6. Crear Order
            var order = new Order(orderDto.UserEmail, locationData, productsOrdered, delivery, subtotal);

            //7.Guardar Orden
            var result = await orderRepository.InsertAsync(order);
            if (result == null)
            {
                throw new NotFoundException($"Orden no generada, intentelo mas tarde");
            }

            //Borrar Carrito
            //await basketRepository.DeleteBasketAsync(orderDto.BasketId);

            var finalOrder = mapper.Map<OrderDetailedDto>(order);
            return finalOrder;
        }

        public async Task<bool> CancelOrderAsync(Guid id)
        {
            var currentDate= DateTime.Now;
            var order = await orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                throw new NotFoundException($"La Orden con Id:{id} no existe");
            }

            var daysOff = currentDate.Day - order.CreationDate.Day;

            if (daysOff > 20)
            {
                throw new ValidationException($"La Orden con Id: {id} no puede ser cancelada. El plazo de 15 dias para cancelacion ha expirado");
            }

            order.Status = Status.Cancelar;
            order.ModifiedDate = DateTime.Now;
            order = await orderRepository.UpdateAsync(order);

            return await orderRepository.DeleteAsync(order.Id);
        }
    }
}
