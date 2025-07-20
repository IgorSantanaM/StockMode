namespace StockMode.Application.Features.Sales.Dtos;

public record SaleItemDetailsDto(
    int Id, 
    int VariationId,
    int Quantity,
    decimal PriceAtSale);
