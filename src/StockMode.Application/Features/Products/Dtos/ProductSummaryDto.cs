namespace StockMode.Application.Features.Products.Dtos;

public record ProductSummaryDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; }
    public long VariationCount { get; init; }
    public decimal? MinSalePrice { get; init; }
    public decimal? MaxSalePrice { get; init; }
    public decimal? AverageCostPrice { get; init; }
    public long TotalStockQuantity { get; init; }
}
