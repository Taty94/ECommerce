using AutoMapper;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities.Order;
using Course.ECommerce.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Aplication.ServicesImpl
{
    public class DeliveryApplication : IDeliveryApplication
    {
        private readonly IGenericRepository<Delivery> deliveryRepository;
        private readonly IMapper mapper;
        private readonly IValidator<CreateDeliveryDto> validator;

        public DeliveryApplication(IGenericRepository<Delivery> deliveryRepository, IMapper mapper , IValidator<CreateDeliveryDto> validator)
        {
            this.deliveryRepository = deliveryRepository;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<ICollection<DeliveryDto>> GetAsync()
        {
            var query = await deliveryRepository.GetAllAsync();

            #region automapper
            var result = mapper.Map<ICollection<DeliveryDto>>(query);
            #endregion

            return result;
        }

        public async Task<DeliveryDto> GetByIdAsync(string id)
        {
            var delivery = await deliveryRepository.GetByIdAsync(id);
            
            if (delivery == null)
            {
                throw new NotFoundException($"Entrega con Id:{id} no existe");
            }
            #region automapper
            return mapper.Map<DeliveryDto>(delivery);
            #endregion
        }

        public async Task<DeliveryDto> InsertAsync(CreateDeliveryDto deliveryDto)
        {
            #region validator
            await validator.ValidateAndThrowAsync(deliveryDto);
            #endregion

            #region automapper
            var delivery = mapper.Map<Delivery>(deliveryDto);
            delivery.ModifiedDate = DateTime.Now;
            #endregion

            var result = await deliveryRepository.InsertAsync(delivery);
            return await GetByIdAsync(result.Id);
        }

        public async Task<DeliveryDto> UpdateAsync(string id, CreateDeliveryDto deliveryDto)
        {
            #region validator
            await validator.ValidateAndThrowAsync(deliveryDto);
            #endregion

            var delivery = await deliveryRepository.GetByIdAsync(id);

            if (delivery == null)
            {
                throw new NotFoundException($"Entrega con Id:{id} no existe");
            }

            #region automapper
            delivery = mapper.Map(deliveryDto, delivery);
            delivery.ModifiedDate = DateTime.Now;
            #endregion

            var result = await deliveryRepository.UpdateAsync(delivery);
            return await GetByIdAsync(result.Id);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var isFound = await deliveryRepository.DeleteAsync(id);
            if (!isFound) throw new NotFoundException($"Entrega con Id:{id} no se elimino, no existe");
            return isFound;
        }
    }
}
