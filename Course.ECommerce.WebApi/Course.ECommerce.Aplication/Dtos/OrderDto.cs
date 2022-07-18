using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Domain.Entities.Order;

namespace Course.ECommerce.Aplication.Services
{
    public class OrderDto
    {
        public string UserEmail { get; set; }
        public string FullName { get; set; }
        public string CityAddress { get; set; }
        public string Phone { get; set; }
        public ICollection<OrderDetailedDto> Orders { get; set; }
        public int Total { get; set; }

    }
}