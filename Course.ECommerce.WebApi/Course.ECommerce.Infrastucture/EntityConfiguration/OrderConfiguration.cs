using Course.ECommerce.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Infrastructure.EntityConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.UserEmail)
                .IsRequired();

            builder.Property(o => o.Status)
                .HasConversion(
                    o=>o.ToString(),
                    o=>(Status) Enum.Parse(typeof(Status),o)
                );

            builder.Property(o => o.Subtotal)
                .HasColumnType("decimal(18,2)");

            //relation
            builder.HasMany(o=>o.ItemsOrdered)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
