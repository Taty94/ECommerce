using Course.ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(Guid Id);
    }
}
