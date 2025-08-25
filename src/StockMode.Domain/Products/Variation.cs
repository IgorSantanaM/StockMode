using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMode.Domain.Products
{
    public class Variation : Entity<int>
    {
        public int ProductId { get; private set; }
        public string Name { get; private set; }
        public string Sku { get; private  set; }
        public decimal CostPrice { get; private set; }
        public decimal SalePrice { get; private set; }
        public int StockQuantity { get; private set; }
        public Product Product { get; set; }

        private Variation()
        { }

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

        public void Update(string name, string sku, decimal costPrice, decimal salePrice, int stock)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Variation name cannot be empty.");
            if (string.IsNullOrWhiteSpace(sku)) throw new DomainException("Variation SKU cannot be empty.");
            if (salePrice < 0 || costPrice < 0) throw new DomainException("Prices cannot be negative.");
            if (stock < 0) throw new DomainException("Stock cannot be negative.");

            Name = name;
            Sku = sku;
            CostPrice = costPrice;
            SalePrice = salePrice;
            StockQuantity = stock;
            // add domain event - send email if the price is lower than the cost price
        }
    }
}
