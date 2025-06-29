using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using StockMode.Domain.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMode.Domain.Sales
{
    public class SaleItem : Entity<int>
    {
        [Required]
        public int SaleId { get; set; }

        [Required]
        public int VariationId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PriceAtSale { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }

        [ForeignKey("VariationId")]
        public Variation Variation { get; set; }

        private SaleItem() { }
        public SaleItem(int variationId, int quantity, decimal priceAtSale)
        {
            if (variationId <= 0)
                throw new DomainException("Invalid Variation ID provided for Sale Item.");

            if (quantity <= 0)
                throw new DomainException("Sale Item quantity must be greater than zero.");

            if (priceAtSale < 0)
                throw new DomainException("Sale Item price cannot be negative.");

            VariationId = variationId;
            Quantity = quantity;
            PriceAtSale = priceAtSale;
        }
        public void IncreaseQuantity(int additionalQuantity)
        {
            if (additionalQuantity <= 0)
                throw new DomainException("Quantity to add must be greater than zero.");

            Quantity += additionalQuantity;
        }

    }
}
