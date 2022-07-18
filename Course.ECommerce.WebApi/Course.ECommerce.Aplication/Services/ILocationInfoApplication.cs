using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Domain.Entities.Order;

namespace Course.ECommerce.Aplication.Services
{
    public interface ILocationInfoApplication
    {
        Task<LocationInfo> GetLocationInfoAsync(string email);
        Task<LocationInfo> UpdateLocationInfoAsync(CreateLocationInfoDto locationInfoDto);
        Task<bool> DeleteLocationInfoAsync(string email);
    }
}
