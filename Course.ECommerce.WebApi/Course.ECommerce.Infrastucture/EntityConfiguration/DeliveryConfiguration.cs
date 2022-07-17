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
    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("DeliveryMethod");
            builder.HasKey(d => d.Id);

            builder.Property(b => b.Id)
            .HasMaxLength(4)
            .IsRequired();

            builder.Property(d => d.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(d => d.Description)
                .HasMaxLength(256);

            builder.Property(d => d.Price)
                .HasColumnType("decimal(18,2)");            
        }
    }
}
