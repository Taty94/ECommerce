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
    public class ProductBrandRepository : IProductBrandRepository
    {
        private readonly ECommerceDbContext context;

        public ProductBrandRepository(ECommerceDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<ProductBrand>> GetAsync()
        {
            return await context.ProductBrand.ToListAsync();
        }
    }
}
