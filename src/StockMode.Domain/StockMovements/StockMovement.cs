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
        public int VariationId { get; set; }

        public int? SaleId { get; set; }

        public StockMovementType Type { get; set; }

        public int Quantity { get; set; }

        public int StockAfterMovement { get; set; }

        public string? Note { get; set; }

        public string? Observation { get; private set; }

        public DateTime MovementDate { get; set; } = DateTime.Now;

        public Variation Variation { get; set; }

        public int? CustomerId { get; set; }

        public int? SupplierId { get; set; }


        private StockMovement() { }

        private StockMovement(int variationId, int quantity, StockMovementType type, int stockAfterMovement, int? saleId, string? observation, int entityId)
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

            if(type is StockMovementType.SaleExit or StockMovementType.LossAdjustment)
                CustomerId = entityId;

            else if (type is StockMovementType.PurchaseEntry or StockMovementType.PositiveAdjustment)
                SupplierId = entityId;
        }

        public static StockMovement CreateForSale(int variationId, int quantitySold, int stockAfter, int saleId, int customerId) =>
             new StockMovement(variationId, -Math.Abs(quantitySold), StockMovementType.SaleExit, stockAfter, saleId, null, customerId);

        public static StockMovement CreateForPurchase(int variationId, int quantityReceived, int stockAfter, int supplierId) =>
             new StockMovement(variationId, Math.Abs(quantityReceived), StockMovementType.PurchaseEntry, stockAfter, null, "Stock received from supplier.", supplierId);

        public static StockMovement CreateForAdjustment(int variationId, int quantityAdjusted, int stockAfter, string reason, int entityId)
        {
            var type = quantityAdjusted > 0 ? StockMovementType.PositiveAdjustment : StockMovementType.LossAdjustment;
            return new StockMovement(variationId, quantityAdjusted, type, stockAfter, null, reason, entityId);
        }
    }
}
