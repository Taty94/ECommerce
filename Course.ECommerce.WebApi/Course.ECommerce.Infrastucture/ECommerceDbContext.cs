using Course.ECommerce.Domain;
using Course.ECommerce.Domain.Entities;
using Course.ECommerce.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Course.ECommerce.Infrastructure
{
    public class ECommerceDbContext :DbContext
    {
        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Catalogue> Catalogue { get; set; }
        public DbSet<Product> Product { get; set; } 
        public DbSet<ProductBrand> ProductBrand { get; set; } 
        public DbSet<ProductType> ProductType { get; set; } 
        public DbSet<Order> Order { get; set; } 
        public DbSet<ItemOrdered> ItemOrdered { get; set; } 
        public DbSet<Delivery> DeliveryMethod { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //1. configurar el proveedor
            //2. Configurar la conexion
            //var connection = @"Server=(localdb)\mssqllocaldb;Database=ECommerce;Trusted_Connection=True";
            //optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Llamamos las configuraciones 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //llamo a todas las configuraciones 
        }
    }
}
