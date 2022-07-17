using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities.Order;
using Course.ECommerce.Domain.Repositories;

namespace Course.ECommerce.Aplication.ServicesImpl
{
    public class LocationInfoApplication : ILocationInfoApplication
    {
        private readonly ILocationInfoRepository locationInfoRepository;

        public LocationInfoApplication(ILocationInfoRepository locationInfoRepository)
        {
            this.locationInfoRepository = locationInfoRepository;
        }

        public async Task<LocationInfo> GetLocationInfoAsync(string email)
        {
            var locationInfo = await locationInfoRepository.GetLocationInfoAsync(email);
            #region NotFoundException
            if (locationInfo == null)
            {
                throw new NotFoundException($"El usuario con Email:{email} no existe");
            }
            #endregion

            return locationInfo;
            
        }

        public async Task<LocationInfo> UpdateLocationInfoAsync(LocationInfo locationInfo)
        {
            var updateLocationInfo = await locationInfoRepository.UpdateLocationInfoAsync(locationInfo);
            return updateLocationInfo;
        }

        public async Task<bool> DeleteLocationInfoAsync(string email)
        {
            return await locationInfoRepository.DeleteLocationInfoAsync(email);
        }
    }
}
