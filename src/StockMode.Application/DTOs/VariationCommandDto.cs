namespace StockMode.Application.DTOs
{
    public record VariationCommandDto(
        string Name,
        string Sku,
        decimal CostPrice,
        decimal SalePrice,
        int StockQuantity
    );
}
