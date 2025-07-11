﻿using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using StockMode.Domain.Products.Events;

namespace StockMode.Domain.Products
{
    public class Product : Entity<int>, IAggregateRoot
    {
        private readonly List<Variation> _variations = new();
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

        public void UpdateDetails(string newName, string? newDescription)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new DomainException("Product name cannot be empty.");

            Name = newName;
            Description = newDescription;
            // Add domainevent
        }
        public void Activate()
        {
            if (!_variations.Any())
                throw new DomainException("Cannot activate a product with no variations.");

            var productAddedEvent = new ProductAddedEvent(this.Id, Name, Description, _variations.AsReadOnly());
            AddDomainEvent(productAddedEvent);

            IsActive = true;


        }

        public void Deactivate() =>
            IsActive = false;

        public void AddVariation(string name, string sku, decimal costPrice, decimal salePrice, int initialStock)
        {
            if (!IsActive)
                throw new DomainException("Cannot add a variation to an inactive product.");

            if (_variations.Any(v => v.Sku == sku))
                throw new DomainException($"A variation with SKU '{sku}' already exists for this product.");

            var newVariation = new Variation(this.Id, name, sku, costPrice, salePrice, initialStock);
            _variations.Add(newVariation);
        }
    }
}
