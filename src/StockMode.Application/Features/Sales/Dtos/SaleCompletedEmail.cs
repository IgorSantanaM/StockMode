using StockMode.Domain.Enums;

namespace StockMode.Application.Features.Sales.Dtos
{
    public record SaleCompletedEmail(
        int SaleId,
        string Email,
        decimal TotalPrice,
        decimal Discount,
        decimal FinalPrice, 
        PaymentMethod PaymentMethod,
        DateTime SaleDate,
        IReadOnlyCollection<SaleItemDetailsDto> Items);
}
