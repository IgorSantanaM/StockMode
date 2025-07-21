using StockMode.Domain.Enums;

namespace StockMode.Application.Features.Sales.Dtos;

public record SaleSummaryDto(
    int Id,
    DateTime SaleDate,
    SaleStatus Status,
    PaymentMethod PaymentMethod,
    decimal FinalPrice,
    long ItemCount);