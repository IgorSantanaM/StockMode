namespace StockMode.Application.Features.Products.Dtos;

public record ProductDetailsDto(
    int Id,
    string Name,
    string? Description,
    bool IsActive,
    IReadOnlyCollection<VariationDetailDto> Variations);
