using Course.ECommerce.Domain.Repositories;
using Course.ECommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Course.ECommerce.Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddTransient(typeof(ILocationInfoRepository), typeof(LocationInfoRepository));
            //AGREGAR CONEXION A BDD
            services.AddDbContext<ECommerceDbContext>(options =>{ options.UseSqlServer(config.GetConnectionString("ECommerce"));});

            //Agregar Redis server
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(config.
                    GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            return services;
        }
    }
}
