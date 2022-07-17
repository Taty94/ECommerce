using Course.ECommerce.Aplication.Services.BasketService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.ECommerce.WebApi.Controllers.Basket
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
        public async Task<Domain.Entities.Basket.Basket> GetBasketAsync(string basketId)
        {
            return await basketApp.GetBasketAsync(basketId);
        }

        [HttpPost]
        public async Task<Domain.Entities.Basket.Basket> UpdateBasketAsync(Domain.Entities.Basket.Basket basket)
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
