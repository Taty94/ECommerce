using Course.ECommerce.Aplication.Services.BasketService;
using Course.ECommerce.Domain.Entities.Basket;
using Course.ECommerce.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Aplication.ServicesImpl.BasketServiceImpl
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

            //#region NotFoundException
            //if (basket == null)
            //{
            //    throw new NotFoundException($"Carrito con Id:{basketId} no existe");
            //}
            //#endregion

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
