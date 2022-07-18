using Course.ECommerce.Aplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Aplication.Services
{
    public interface IDeliveryApplication
    {
        Task<ICollection<DeliveryDto>> GetAsync();
        Task<DeliveryDto> GetByIdAsync(string id);
        Task<DeliveryDto> InsertAsync(CreateDeliveryDto deliveryDto);
        Task<DeliveryDto> UpdateAsync(string id, CreateDeliveryDto deliveryDto);
        Task<bool> DeleteAsync(string id);
    }
}
