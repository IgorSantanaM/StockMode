namespace StockMode.Application.Features.Sales.Dtos;

public record SaleSummaryDto(
    int Id,
    DateTime SaleDate,
    string Status,
    string PaymentMethod,
    decimal FinalPrice,
    long ItemCount);