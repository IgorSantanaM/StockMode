using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMode.Domain.Sales;
using StockMode.Infra.Data.Extensions;

namespace StockMode.Infra.Data.Mappings
{
    public class SaleItemMapping : EntityTypeConfiguration<SaleItem>
    {
        public override void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.Id);

            builder.Property(si => si.Quantity)
                   .IsRequired();

            builder.Property(si => si.PriceAtSale)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(si => si.Sale)
                   .WithMany(s => s.Items)
                   .HasForeignKey(si => si.SaleId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(si => si.Variation)
                .WithMany()
                .HasForeignKey(si => si.VariationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(si => si.DomainEvents);
        }
    }
}
