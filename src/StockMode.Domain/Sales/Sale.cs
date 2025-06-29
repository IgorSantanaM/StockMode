using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using StockMode.Domain.Enums;
using StockMode.Domain.Sales.Events;

namespace StockMode.Domain.Sales
{
    public class Sale : Entity<int>, IAggregateRoot
    {
        private readonly List<SaleItem> _saleItems = new();
        public decimal TotalPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal FinalPrice { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public SaleStatus Status { get; private set; }
        public DateTime SaleDate { get; private set; }
        public IReadOnlyCollection<SaleItem> SaleItems => _saleItems.AsReadOnly();

        private Sale()
        { }

        public Sale(PaymentMethod paymentMethod)
        {
            Status = SaleStatus.PaymentPending;
            PaymentMethod = paymentMethod;
            SaleDate = DateTime.UtcNow;
        }

        public void AddItem(SaleItem newItem)
        {
            if (Status != SaleStatus.PaymentPending)
                throw new DomainException("Cannot add items to a sale that is not pending.");

            if (newItem.Quantity <= 0)
                throw new DomainException("Item quantity must be greater than zero.");

            SaleItem? existingItem = _saleItems.FirstOrDefault(i => i.VariationId == newItem.VariationId);
            if (existingItem! != null!)
            {
                existingItem.IncreaseQuantity(newItem.Quantity);
            }
            else
            {
                _saleItems.Add(newItem);
            }

            RecalculateTotals();
        }

        public void ApplyDiscount(decimal discountAmount)
        {
            if (Status != SaleStatus.PaymentPending)
                throw new DomainException("Cannot apply a discount to a sale that is not pending.");

            if (discountAmount < 0)
                throw new DomainException("Discount cannot be negative.");

            if (discountAmount > TotalPrice)
                throw new DomainException("Discount cannot be greater than the total price of the sale.");

            Discount = discountAmount;
            RecalculateTotals();
        }

        public void CompleteSale()
        {
            if (Status != SaleStatus.PaymentPending)
                throw new DomainException("Only a pending sale can be completed.");
            if (!_saleItems.Any())
                throw new DomainException("Cannot complete a sale with no items.");

            Status = SaleStatus.Completed;
            var saleCompletedEvent = new SaleCompletedEvent(this.Id, this.FinalPrice, this._saleItems);
            AddDomainEvent(saleCompletedEvent);
        }

        public void CancelSale()
        {
            if (Status == SaleStatus.Completed)
                throw new DomainException("Cannot cancel a sale that has already been completed.");

            Status = SaleStatus.Cancelled;

        }
        private void RecalculateTotals()
        {
            TotalPrice = _saleItems.Sum(item => item.PriceAtSale * item.Quantity);
            FinalPrice = TotalPrice - Discount;
        }
    }
}
