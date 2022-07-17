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
    public class ItemOrderedConfiguration : IEntityTypeConfiguration<ItemOrdered>
    {
        public void Configure(EntityTypeBuilder<ItemOrdered> builder)
        {
            builder.ToTable("ItemOrdered");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
            .HasMaxLength(4)
            .IsRequired();

            builder.Property(it => it.Name)
                .HasMaxLength(100);

            builder.Property(it => it.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(it => it.Quantiy)
                .IsRequired();
        }
    }
}
