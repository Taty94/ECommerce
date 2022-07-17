using Course.ECommerce.Domain.Entities.BasketClasses;

namespace Course.ECommerce.Aplication.Services
{
    public interface IBasketApplication
    {
        Task<Basket> GetBasketAsync(string basketId);
        Task<Basket> UpdateBasketAsync(Basket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
