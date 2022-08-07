using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course.ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase ,IOrderApplication
    {
        private readonly IOrderApplication orderApp;

        public OrderController(IOrderApplication orderApp)
        {
            this.orderApp = orderApp;
        }

        [HttpGet("delivery")]
        public async Task<ICollection<DeliveryDto>> GetAllDeliveryByUserAsync(string userEmail)
        {
            return await orderApp.GetAllDeliveryByUserAsync(userEmail);
        }

        [HttpGet]
        public async Task<OrderDto> GetAllOrdersByUserAsync(string userEmail, int offset = 0, int limit = 3, string sort = "Date", string order = "asc")
        {
            return await orderApp.GetAllOrdersByUserAsync(userEmail,offset,limit,sort,order);
        }

        [HttpGet("{id}")]
        public async Task<OrderDetailedDto> GetOrderByIdAsync(Guid id, string userEmail)
        {
            return await orderApp.GetOrderByIdAsync(id, userEmail);
        }

        [HttpPost]
        public async Task<OrderDetailedDto> InsertOrderAsync(CreateOrderDto orderDto)
        {
            return await orderApp.InsertOrderAsync(orderDto);
        }

        [HttpDelete]
        public async Task<bool> CancelOrderAsync(Guid id)
        {
            return await orderApp.CancelOrderAsync(id);
        }
    }
}
