using Course.ECommerce.Domain.Entities.Order;

namespace Course.ECommerce.Domain.Repositories
{
    public interface ILocationInfoRepository
    {
        Task<LocationInfo> GetLocationInfoAsync(string email);
        Task<LocationInfo> UpdateLocationInfoAsync(LocationInfo locationInfo);
        Task<bool> DeleteLocationInfoAsync(string email);
    }
}
