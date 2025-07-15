namespace StockMode.Application.Features.Products.Dtos;

public record VariationDetailDto(
    int Id,
    string Name,
    string Sku,
    decimal CostPrice,
    decimal SalePrice,
    int StockQuantity);
