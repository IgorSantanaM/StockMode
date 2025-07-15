namespace StockMode.Application.Features.Products.Dtos;

public record VariationCommandDto(
    string Name,
    string Sku,
    decimal CostPrice,
    decimal SalePrice,
    int StockQuantity
);
