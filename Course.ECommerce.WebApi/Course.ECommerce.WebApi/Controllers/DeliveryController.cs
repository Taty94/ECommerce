using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeliveryController : ControllerBase, IDeliveryApplication
    {
        private readonly IDeliveryApplication deliveryApp;

        public DeliveryController(IDeliveryApplication deliveryApp)
        {
            this.deliveryApp = deliveryApp;
        }

        [HttpGet]
        public async Task<ICollection<DeliveryDto>> GetAsync()
        {
            return await deliveryApp.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<DeliveryDto> GetByIdAsync(string id)
        {
            return await deliveryApp.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<DeliveryDto> InsertAsync(CreateDeliveryDto deliveryDto)
        {
            return await deliveryApp.InsertAsync(deliveryDto);
        }

        [HttpPut]
        public async Task<DeliveryDto> UpdateAsync(string id, CreateDeliveryDto deliveryDto)
        {
           return await deliveryApp.UpdateAsync(id, deliveryDto);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(string id)
        {
            return await deliveryApp.DeleteAsync(id);
        }
    }
}
