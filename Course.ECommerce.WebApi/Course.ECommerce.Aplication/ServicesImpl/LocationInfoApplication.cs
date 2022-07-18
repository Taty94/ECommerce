using AutoMapper;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities;
using Course.ECommerce.Domain.Entities.Order;
using Course.ECommerce.Domain.Repositories;
using FluentValidation;

namespace Course.ECommerce.Aplication.ServicesImpl
{
    public class LocationInfoApplication : ILocationInfoApplication
    {
        private readonly ILocationInfoRepository locationInfoRepository;
        private readonly IMapper mapper;
        private readonly IValidator<CreateLocationInfoDto> validator;

        public LocationInfoApplication(ILocationInfoRepository locationInfoRepository , IMapper mapper, IValidator<CreateLocationInfoDto> validator)
        {
            this.locationInfoRepository = locationInfoRepository;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<LocationInfo> GetLocationInfoAsync(string email)
        {
            var locationInfo = await locationInfoRepository.GetLocationInfoAsync(email);
            #region NotFoundException
            if (locationInfo == null)
            {
                throw new NotFoundException($"La informacion del user con Email:{email} no existe");
            }
            #endregion

            return locationInfo;
            
        }

        public async Task<LocationInfo> UpdateLocationInfoAsync(CreateLocationInfoDto locationInfoDto)
        {
            await validator.ValidateAndThrowAsync(locationInfoDto);

            var locationInfo = new LocationInfo();
            locationInfo = mapper.Map<LocationInfo>(locationInfoDto);
            var updateLocationInfo = await locationInfoRepository.UpdateLocationInfoAsync(locationInfo);

            if (updateLocationInfo == null)
            {
                throw new NotFoundException($"La informacion del user con Email:{locationInfo.Email} no se modifico, no se pudo encontrar");
            }

            return updateLocationInfo;
        }

        public async Task<bool> DeleteLocationInfoAsync(string email)
        {
            var isFound = await locationInfoRepository.DeleteLocationInfoAsync(email);
            if (!isFound) throw new NotFoundException($"La informacion del user con Email:{email} no se elimino, no existe");
            return isFound;
        }
    }
}
