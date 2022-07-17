using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities.BasketClasses;
using Course.ECommerce.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Aplication.ServicesImpl
{
    public class BasketApplication : IBasketApplication
    {
        private readonly IBasketRepository basketRepository;

        public BasketApplication(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        public async Task<Basket> GetBasketAsync(string basketId)
        {
            var basket = await basketRepository.GetBasketAsync(basketId);

            return basket ?? new Basket(basketId);
        }

        public async Task<Basket> UpdateBasketAsync(Basket basket)
        {
            var updateBasket = await basketRepository.UpdateBasketAsync(basket);
            return updateBasket;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await basketRepository.DeleteBasketAsync(basketId);
        }
    }
}
