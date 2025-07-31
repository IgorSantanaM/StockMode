namespace StockMode.Application.Features.Sales.Dtos;

public record SaleItemDetailsDto(
    int Id, 
    string Name,
    int VariationId,
    int Quantity,
    decimal PriceAtSale);
