using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Aplication.ServicesImpl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Aplication
{
    public static class AplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddAplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient(typeof(IProductApplication), typeof(ProductApplication));
            services.AddTransient(typeof(IProductTypeApplication), typeof(ProductTypeApplication));
            services.AddTransient(typeof(IProductBrandApplication), typeof(ProductBrandApplication));

            //automapper
            //agrega todos los profiles que existen el este proyecto
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
