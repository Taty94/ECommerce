using AutoMapper;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities;
using Course.ECommerce.Domain.Entities.Order;

namespace Course.ECommerce.Aplication.Helpers
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            //CreateMap<Origen,Destino>
            CreateMap<ProductDto, Product>();
            CreateMap<CreateProductDto, Product>();
            
            //CreateMap<Product, ProductDto>().ForMember(p=>p.ProductBrand,x=>x.MapFrom(org=>org.ProductBrand.Description))
            //                                .ForMember(p=>p.ProductType, x=>x.MapFrom(org=>org.ProductType.Description));

            CreateMap<ProductTypeDto, ProductType>();
            CreateMap<ProductType, ProductTypeDto>();
            CreateMap<CreateProductTypeDto, ProductType>();

            CreateMap<ProductBrandDto, ProductBrand>();
            CreateMap<ProductBrand, ProductBrandDto>();
            CreateMap<CreateProductBrandDto, ProductBrand>();


            //Entrega
            CreateMap<Delivery, DeliveryDto>();
            CreateMap<DeliveryDto, Delivery>();
            CreateMap<CreateDeliveryDto, Delivery>();


            //Ordenes
            CreateMap<CreateLocationInfoDto, LocationInfo>()
                .ForMember(d => d.FullName, d => d
                .MapFrom(org => string.Format("{0} {1}", org.Name, org.LastName)))
                .ForMember(d => d.Adress, d => d
                .MapFrom(org => string.Format("{0} y {1}", org.MainStreet, org.SecondaryStreet)));

            CreateMap<Order, OrderDto>();
            CreateMap<Order, OrderDetailedDto>()
                .ForMember(d => d.Description, d => d
                .MapFrom(org => org.DeliveryMethod.Name))
                .ForMember(d => d.Price, d => d
                .MapFrom(org => org.DeliveryMethod.Price));


            //ItemsOrdered
            CreateMap<ItemOrdered, ItemOrderedDto>();
        }
    }
}
