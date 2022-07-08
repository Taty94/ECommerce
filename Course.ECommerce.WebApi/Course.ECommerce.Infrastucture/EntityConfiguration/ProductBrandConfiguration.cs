using Course.ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.ECommerce.Infrastructure.EntityConfiguration
{
    public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.ToTable("ProductBrand");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
            .HasMaxLength(4)
            .IsRequired();

            builder.Property(b => b.Description)
            .HasMaxLength(256)
            .IsRequired();


        }
    }
}
