using Course.ECommerce.Domain.Entities.BasketClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Domain.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasketAsync(string basketId);
        Task<Basket> UpdateBasketAsync(Basket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
