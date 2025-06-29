using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using StockMode.Domain.Enums;
using StockMode.Domain.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMode.Domain.StockMovements
{
    public class StockMovement : Entity<int>, IAggregateRoot
    {
        [Required]
        public int VariationId { get; set; }

        public int? SaleId { get; set; }

        [Required]
        public StockMovementType Type { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int StockAfterMovement { get; set; }

        public string? Note { get; set; }

        public string? Observation { get; private set; }

        public DateTime MovementDate { get; set; } = DateTime.Now;

        [ForeignKey("VariationId")]
        public Variation Variation { get; set; }

        private StockMovement() { }

        private StockMovement(int variationId, int quantity, StockMovementType type, int stockAfterMovement, int? saleId, string? observation)
        {
            if (variationId <= 0)
                throw new DomainException("Stock Movement must be associated with a valid product variation.");

            if (quantity == 0)
                throw new DomainException("Stock Movement quantity cannot be zero.");

            if ((type == StockMovementType.SaleExit || type == StockMovementType.LossAdjustment) && quantity > 0)
                throw new DomainException("Exit or loss adjustments must have a negative quantity.");

            if ((type == StockMovementType.PurchaseEntry || type == StockMovementType.PositiveAdjustment) && quantity < 0)
                throw new DomainException("Entry or positive adjustments must have a positive quantity.");

            if ((type == StockMovementType.LossAdjustment || type == StockMovementType.PositiveAdjustment) && string.IsNullOrWhiteSpace(observation))
                throw new DomainException("A reason is required for all manual stock adjustments.");

            VariationId = variationId;
            Quantity = quantity;
            Type = type;
            StockAfterMovement = stockAfterMovement;
            SaleId = saleId;
            Observation = observation;
            MovementDate = DateTime.UtcNow;
        }

        public static StockMovement CreateForSale(int variationId, int quantitySold, int stockAfter, int saleId) =>
             new StockMovement(variationId, -Math.Abs(quantitySold), StockMovementType.SaleExit, stockAfter, saleId, null);

        public static StockMovement CreateForPurchase(int variationId, int quantityReceived, int stockAfter) =>
             new StockMovement(variationId, Math.Abs(quantityReceived), StockMovementType.PurchaseEntry, stockAfter, null, "Stock received from supplier.");

        public static StockMovement CreateForAdjustment(int variationId, int quantityAdjusted, int stockAfter, string reason)
        {
            var type = quantityAdjusted > 0 ? StockMovementType.PositiveAdjustment : StockMovementType.LossAdjustment;
            return new StockMovement(variationId, quantityAdjusted, type, stockAfter, null, reason);
        }
    }
}
