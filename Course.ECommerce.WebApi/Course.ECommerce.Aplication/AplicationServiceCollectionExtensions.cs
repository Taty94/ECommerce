using Course.ECommerce.Aplication.Helpers;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Aplication.ServicesImpl;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Course.ECommerce.Aplication
{
    public static class AplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddAplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient(typeof(IProductApplication), typeof(ProductApplication));
            services.AddTransient(typeof(IProductTypeApplication), typeof(ProductTypeApplication));
            services.AddTransient(typeof(IProductBrandApplication), typeof(ProductBrandApplication));
            services.AddTransient(typeof(IBasketApplication), typeof(BasketApplication));
            services.AddTransient(typeof(ILocationInfoApplication), typeof(LocationInfoApplication));
            //automapper
            //agrega todos los profiles que existen el este proyecto
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //validaciones
            services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>(); //cualquier clase, tipo pertenece a un ensamblador
                                                                                       //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());//ambas buscan todos los validadores y los inyectan

            return services;
        }
    }
}
