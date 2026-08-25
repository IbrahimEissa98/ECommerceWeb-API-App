using ECommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Infrastructure.Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
                .HasMaxLength(100);
        builder.Property(p => p.Description)
                .HasMaxLength(500);
        builder.Property(p => p.PictureUrl)
                .HasMaxLength(200);
        builder.Property(p => p.Price)
                .HasPrecision(18, 2);

        builder.HasOne(p => p.ProductBrand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(p => p.ProductType)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.TypeId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.Price);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CHK_ProductPrice", "Price > 0");
        });
    }
}
