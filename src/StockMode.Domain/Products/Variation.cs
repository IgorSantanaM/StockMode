using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMode.Domain.Products
{
    public class Variation : Entity<int>
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int StockQuantity { get; set; }
        public Product Product { get; set; }

        private Variation()
        {
        }

        public Variation(int productId, string name, string sku, decimal costPrice, decimal salePrice, int initialStock)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Variation name cannot be empty.");
            if (string.IsNullOrWhiteSpace(sku)) throw new DomainException("Variation SKU cannot be empty.");
            if (salePrice < 0) throw new DomainException("Sale price cannot be negative.");
            if (initialStock < 0) throw new DomainException("Initial stock cannot be negative.");

            ProductId = productId;
            Name = name;
            Sku = sku;
            CostPrice = costPrice;
            SalePrice = salePrice;
            StockQuantity = initialStock;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity to increase must be positive.");

            StockQuantity += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity to decrease must be positive.");

            if (StockQuantity < quantity)
                throw new DomainException($"Not enough stock for SKU '{Sku}'. Available: {StockQuantity}, Requested: {quantity}.");

            StockQuantity -= quantity;
        }

        public void UpdatePricing(decimal newCostPrice, decimal newSalePrice)
        {
            if (newSalePrice < 0 || newCostPrice < 0)
                throw new DomainException("Prices cannot be negative.");

            CostPrice = newCostPrice;
            SalePrice = newSalePrice;
            // add domain event - send email if the price is lower than the cost price
        }
    }
}
