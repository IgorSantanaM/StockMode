using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StockMode.Domain.StockMovements;
using StockMode.Infra.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Infra.Data.Mappings
{
    public class StockMovementMapping : EntityTypeConfiguration<StockMovement>
    {
        public override void Map(EntityTypeBuilder<StockMovement> builder)
        {
            builder.ToTable("StockMovements");
            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.Quantity)
                .IsRequired();

            builder.Property(sm => sm.StockAfterMovement)
                .IsRequired();

            builder.Property(sm => sm.Type)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(sm => sm.MovementDate)
                .IsRequired();

            builder.Property(sm => sm.Observation)
                .HasMaxLength(500); 

            builder.Property(sm => sm.Note)
                .HasMaxLength(500); 

            builder.HasOne(sm => sm.Variation)
                .WithMany()
                .HasForeignKey(sm => sm.VariationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(sm => sm.DomainEvents);
        }
    }
}
