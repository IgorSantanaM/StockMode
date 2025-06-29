using StockMode.Domain.Core.Model;

namespace StockMode.Domain.Products
{
    public class Product : Entity<Product>, IAggregateRoot
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }

        public ICollection<Variation> Variations { get; set; } = new List<Variation>();
    }
}
