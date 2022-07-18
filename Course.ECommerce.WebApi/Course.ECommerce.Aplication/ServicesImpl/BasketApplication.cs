using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities.BasketClasses;
using Course.ECommerce.Domain.Repositories;
using FluentValidation;
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
        private readonly IValidator<Basket> validator;

        public BasketApplication(IBasketRepository basketRepository, IValidator<Basket> validator)
        {
            this.basketRepository = basketRepository;
            this.validator = validator;
        }

        public async Task<Basket> GetBasketAsync(string basketId)
        {
            var basket = await basketRepository.GetBasketAsync(basketId);

            if (basket == null)
            {
                throw new NotFoundException($"El carrito con Id:{basketId} no se pudo encontrar");
            }

            //return basket ?? new Basket(basketId);
            return basket;
        }

        public async Task<Basket> UpdateBasketAsync(Basket basket)
        {
            await validator.ValidateAndThrowAsync(basket);

            var updateBasket = await basketRepository.UpdateBasketAsync(basket);

            if (updateBasket == null)
            {
                throw new NotFoundException($"El carrito con Id:{basket.Id} no se modifico, no se pudo encontrar");
            }

            return updateBasket;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            var isFound = await basketRepository.DeleteBasketAsync(basketId);
            if(!isFound) throw new NotFoundException($"El carrito con Id:{basketId} no se elimino, no existe");
            return isFound;
        }
    }
}
