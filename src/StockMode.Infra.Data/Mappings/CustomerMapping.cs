using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMode.Domain.Customers;
using StockMode.Infra.Data.Extensions;

namespace StockMode.Infra.Data.Mappings
{
    public class CustomerMapping : EntityTypeConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(15);

            builder.OwnsOne(c => c.Address, addressBuilder =>
            {
                addressBuilder.Property(a => a.Street).HasMaxLength(200).HasColumnName("Street").IsRequired();
                addressBuilder.Property(a => a.City).HasMaxLength(100).HasColumnName("City").IsRequired();
                addressBuilder.Property(a => a.State).HasMaxLength(50).HasColumnName("State").IsRequired();
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(10).HasColumnName("ZipCode").IsRequired();
                addressBuilder.Property(a => a.Number).HasMaxLength(10).HasColumnName("Number").IsRequired();
            });

            builder.Ignore(c => c.DomainEvents);
        }
    }
}
