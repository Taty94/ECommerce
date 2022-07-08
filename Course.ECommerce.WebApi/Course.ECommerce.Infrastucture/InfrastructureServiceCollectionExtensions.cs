using Course.ECommerce.Domain.Repositories;
using Course.ECommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Course.ECommerce.Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //AGREGAR CONEXION A BDD
            services.AddDbContext<ECommerceDbContext>(options =>{ options.UseSqlServer(config.GetConnectionString("ECommerce"));});
            return services;
        }
    }
}
