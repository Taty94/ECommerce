using Course.ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.ECommerce.Infrastructure.EntityConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
            .IsRequired();

            builder.Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(b => b.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(b => b.Description)
                .HasMaxLength(256);

            builder.Property(b => b.IsDeleted)
                .IsRequired();

            //relacion uno a muchos
            builder.HasOne(b => b.ProductType)
                .WithMany()
                .HasForeignKey(b => b.ProductTypeId);
                //.OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.ProductBrand)
                .WithMany()
                .HasForeignKey(b => b.ProductBrandId);
                //.OnDelete(DeleteBehavior.Restrict);


        }
    }
}
