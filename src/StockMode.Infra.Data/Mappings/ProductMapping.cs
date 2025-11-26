using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMode.Domain.Products;
using StockMode.Infra.Data.Extensions;

namespace StockMode.Infra.Data.Mappings
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.Description)
                   .HasMaxLength(500);

            builder.Property(p => p.IsActive)
                   .IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Product.Variations));

            navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(p => p.Variations)
                   .WithOne(o => o.Product)
                   .HasForeignKey(e => e.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(p => p.DomainEvents);
        }
    }
}
