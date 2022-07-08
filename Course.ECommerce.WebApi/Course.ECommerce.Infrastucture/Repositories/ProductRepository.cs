using Course.ECommerce.Domain.Entities;
using Course.ECommerce.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext context;

        public ProductRepository(ECommerceDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Product>> GetProductsAsync()
        {
            return await context.Product.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid Id)
        {
            return await context.Product.FindAsync(Id);

        }
    }
}
