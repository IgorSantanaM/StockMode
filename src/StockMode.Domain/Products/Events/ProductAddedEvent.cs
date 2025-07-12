using StockMode.Domain.Core.Events;

namespace StockMode.Domain.Products.Events;

public record VariationAddedDto(int VariationId, string Name);
public record ProductAddedEvent(int ProductId,
            string? Name,
            string? Description,
            IReadOnlyCollection<VariationAddedDto> Variations)
            : Event
{
    public ProductAddedEvent(int productId, string name, string description, IReadOnlyCollection<Variation> variations)
        : this(productId, name, description, variations.Select(item => new VariationAddedDto(item.Id, item.Name)).ToList())
    { }
}
