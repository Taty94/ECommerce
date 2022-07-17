using Course.ECommerce.Domain.Entities.Order;
using Course.ECommerce.Domain.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace Course.ECommerce.Infrastructure.Repositories
{
    public class LocationInfoRepository : ILocationInfoRepository
    {
        public readonly IDatabase database;

        public LocationInfoRepository(IConnectionMultiplexer redis)
        {
            this.database = redis.GetDatabase();
        }

        public async Task<LocationInfo> GetLocationInfoAsync(string email)
        {
            var locationInfo =  await database.StringGetAsync(email);
            return locationInfo.IsNullOrEmpty ? null : JsonSerializer.Deserialize<LocationInfo>(locationInfo);
        }

        public async Task<LocationInfo> UpdateLocationInfoAsync(LocationInfo locationInfo)
        {
            var locationInfoCreated = await database.StringSetAsync(locationInfo.Email,
                JsonSerializer.Serialize(locationInfo), TimeSpan.FromDays(20));
            if (!locationInfoCreated) return null;

            return await GetLocationInfoAsync(locationInfo.Email);
        }

        public async Task<bool> DeleteLocationInfoAsync(string email)
        {
            return await database.KeyDeleteAsync(email);
        }
    }
}
