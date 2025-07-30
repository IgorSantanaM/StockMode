using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMode.Domain.Products;
using StockMode.Infra.Data.Extensions;

namespace StockMode.Infra.Data.Mappings
{
    public class VariationMapping : EntityTypeConfiguration<Variation>
    {
        public override void Configure(EntityTypeBuilder<Variation> builder)
        {
            builder.ToTable("Variations");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(v => v.Sku)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(v => v.CostPrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(v => v.SalePrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(v => v.StockQuantity)
                .IsRequired();

            builder.HasIndex(v => v.Sku)
                .IsUnique();

            builder.HasOne(v => v.Product)
                .WithMany(p => p.Variations)
                .HasForeignKey(v => v.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(v => v.DomainEvents);
        }

    }
}
