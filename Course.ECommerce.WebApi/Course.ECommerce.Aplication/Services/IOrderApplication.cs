using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Aplication.Services
{
    public interface IOrderApplication
    {
        Task<OrderDetailedDto> InsertOrderAsync(CreateOrderDto createOrderDto);
        Task<OrderDto> GetAllOrdersByUserAsync(string userEmail, int offset = 0, int limit = 3, string sort = "Date", string order = "asc");
        Task<OrderDetailedDto> GetOrderByIdAsync(Guid id, string userEmail);
        Task<ICollection<DeliveryDto>> GetAllDeliveryByUserAsync(string userEmail);
        Task<bool> CancelOrderAsync(Guid id);
    }
}
