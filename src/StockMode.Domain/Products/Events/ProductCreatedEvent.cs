using StockMode.Domain.Core.Events;

namespace StockMode.Domain.Products.Events;

public record VariationAddedDto(string Name, string Sku, decimal SalePrice);
public record ProductCreatedEvent(int ProductId,
            string? Name,
            string? Description,
            IReadOnlyCollection<VariationAddedDto> Variations)
            : Event
{
    public ProductCreatedEvent(int productId, string name, string description, IReadOnlyCollection<Variation> variations)
        : this(productId, name, description, variations.Select(item => new VariationAddedDto(item.Name, item.Sku, item.SalePrice)).ToList())
    { }
}
