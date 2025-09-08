using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using StockMode.Domain.Products.Events;

namespace StockMode.Domain.Products
{
    public class Product : Entity<int>, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string? Description { get; protected set; }
        public bool IsActive { get; protected set; }
        public ICollection<Variation> Variations { get; private set; } = new List<Variation>();

        private Product()
        { }

        public Product(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty.");

            Name = name;
            Description = description;
            IsActive = true;
        }


        public void AddToDomainEvent()
        {
            var productCreatedEvent = new ProductCreatedEvent(Id, Name, Description!, Variations.ToList());
            AddDomainEvent(productCreatedEvent);
        }

        public void UpdateDetails(string newName, string? newDescription)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new DomainException("Product name cannot be empty.");

            Name = newName;
            Description = newDescription;
        }

        public void Activate()
        {
            if (!Variations.Any())
                throw new DomainException("Cannot activate a product with no variations.");

            IsActive = true;
        }

        public void Deactivate() =>
            IsActive = false;


        public void RemoveVariation(Variation variation) =>
            Variations.Remove(variation);

        public void AddVariation(string name, string sku, decimal costPrice, decimal salePrice, int initialStock)
        {
            if (!IsActive)
                throw new DomainException("Cannot add a variation to an inactive product.");

            if (Variations.Any(v => v.Sku == sku))
                throw new DomainException($"A variation with SKU '{sku}' already exists for this product.");

            var newVariation = new Variation(this.Id, name, sku, costPrice, salePrice, initialStock);
            Variations.Add(newVariation);
        }
    }
}
