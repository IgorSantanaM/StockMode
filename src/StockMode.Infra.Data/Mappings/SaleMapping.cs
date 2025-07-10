using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMode.Domain.Sales;
using StockMode.Infra.Data.Extensions;

namespace StockMode.Infra.Data.Mappings
{
    public class SaleMapping : EntityTypeConfiguration<Sale>
    {
        public override void Map(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(s => s.Discount)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(s => s.FinalPrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(s => s.PaymentMethod)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.SaleDate)
                .IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Sale.Items));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(s => s.Items)
                .WithOne(si => si.Sale) 
                .HasForeignKey(si => si.SaleId) 
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(s => s.DomainEvents);
        }
    }
}
