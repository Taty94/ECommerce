using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities.BasketClasses;
using Microsoft.AspNetCore.Mvc;

namespace Course.ECommerce.WebApi.Controllers.BasketController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase, IBasketApplication
    {
        private readonly IBasketApplication basketApp;

        public BasketController(IBasketApplication basketApp)
        {
            this.basketApp = basketApp;
        }

        [HttpGet]
        public async Task<Basket> GetBasketAsync(string basketId)
        {
            return await basketApp.GetBasketAsync(basketId);
        }

        [HttpPost]
        public async Task<Basket> UpdateBasketAsync(Basket basket)
        {
            return await basketApp.UpdateBasketAsync(basket);
        }

        [HttpDelete]
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await basketApp.DeleteBasketAsync(basketId);
        }
    }
}
