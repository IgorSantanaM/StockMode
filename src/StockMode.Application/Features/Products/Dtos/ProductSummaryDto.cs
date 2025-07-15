namespace StockMode.Application.Features.Products.Dtos;

public record ProductSummaryDto(int Id,
                                string Name,
                                string? Description,
                                bool IsActive,
                                int VariationCount,
                                decimal? MinSalePrice,
                                decimal? MaxSalePrice,
                                decimal? AverageCostPrice,
                                int TotalStockQuantity);
