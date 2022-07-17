using Course.ECommerce.Domain.Entities.BasketClasses;
using Course.ECommerce.Domain.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace Course.ECommerce.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public readonly IDatabase database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            this.database = redis.GetDatabase();
        }

        public async Task<Basket> GetBasketAsync(string basketId)
        {
            var data = await database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(data);
        }

        public async Task<Basket> UpdateBasketAsync(Basket basket)
        {
            var created = await database.StringSetAsync(basket.Id,
                JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));
            if (!created) return null;

            return await GetBasketAsync(basket.Id);


        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await database.KeyDeleteAsync(basketId);
        }
    }
}
