using StockMode.Domain.Core.Events;

namespace StockMode.Domain.Sales.Events
{
    public record SoldItemDto(int VariationId, int Quantity);

    public record SaleCompletedEvent(
        int SaleId,
        decimal FinalAmount,
        IReadOnlyCollection<SoldItemDto> SoldItems,
        int CustomerId
    ) : Event
    {
        public SaleCompletedEvent(int saleId, decimal finalAmount, IReadOnlyCollection<SaleItem> saleItems, int customerId)
            : this(saleId, finalAmount, saleItems.Select(item => new SoldItemDto(item.VariationId, item.Quantity)).ToList(), customerId)
        { }
    }
}
