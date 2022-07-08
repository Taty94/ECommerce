using Course.ECommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace Course.ECommerce.Infrastructure
{
    public class CatalogueRepository : ICatalogueRepository
    {
        private readonly ECommerceDbContext context;

        public CatalogueRepository(ECommerceDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Catalogue>> GetAsync()
        {
            return await context.Catalogue.ToListAsync();
        }
    }
}