namespace StockMode.Application.Features.Sales.Dtos;
public record SaleDetailsDto(
    int Id,
    DateTime SaleDate,
    decimal TotalPrice,
    decimal Discount,
    decimal FinalPrice,
    string PaymentMethod,
    string Status,
    IReadOnlyCollection<SaleItemDetailsDto> SaleItems
    );
