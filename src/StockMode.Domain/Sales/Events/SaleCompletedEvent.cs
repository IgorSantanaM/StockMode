using StockMode.Domain.Core.Events;

namespace StockMode.Domain.Sales.Events
{

    public record SoldItemDto(int VariationId, int Quantity);

    public class SaleCompletedEvent : Event
    {
        public int SaleId { get; }

        public decimal FinalAmount { get; }
        public IReadOnlyCollection<SoldItemDto> SoldItems { get; }

        public SaleCompletedEvent(int saleId, decimal finalAmount, IReadOnlyCollection<SaleItem> saleItems)
        {
            SaleId = saleId;
            FinalAmount = finalAmount;

            SoldItems = saleItems
                .Select(item => new SoldItemDto(item.VariationId, item.Quantity))
                .ToList();
        }
    }
}
