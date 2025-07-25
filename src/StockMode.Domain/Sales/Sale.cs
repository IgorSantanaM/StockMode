using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using StockMode.Domain.Enums;
using StockMode.Domain.Sales.Events;

namespace StockMode.Domain.Sales
{
    public class Sale : Entity<int>, IAggregateRoot
    {
        public decimal TotalPrice { get; private set; }
        public decimal Discount { get; private set; }   
        public decimal FinalPrice { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public SaleStatus Status { get; private set; }
        public DateTime SaleDate { get; private set; }
        public ICollection<SaleItem> Items { get; private set; } = new List<SaleItem>();

        private Sale()
        { }

        public Sale(PaymentMethod paymentMethod)
        {
            if(paymentMethod < PaymentMethod.Pix || paymentMethod > PaymentMethod.StoreCredit)
                throw new DomainException("Invalid payment method.");

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

            SaleItem? existingItem = Items.FirstOrDefault(i => i.VariationId == newItem.VariationId);
            if (existingItem! != null!)
            {
                existingItem.IncreaseQuantity(newItem.Quantity);
            }
            else
            {
                Items.Add(newItem);
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

        public void ChangePaymentMethod(PaymentMethod newPaymentMethod)
        {
            if(newPaymentMethod < PaymentMethod.Pix || newPaymentMethod > PaymentMethod.StoreCredit)
                throw new DomainException("Invalid payment method.");

            if (Status != SaleStatus.PaymentPending)
                throw new DomainException("Cannot change payment method for a sale that is not pending.");

            PaymentMethod = newPaymentMethod;
        }

        public void CompleteSale()
        {
            if (Status != SaleStatus.PaymentPending)
                throw new DomainException("Only a pending sale can be completed.");
            if (!Items.Any())
                throw new DomainException("Cannot complete a sale with no items.");

            Status = SaleStatus.Completed;
            var saleCompletedEvent = new SaleCompletedEvent(Id, FinalPrice, Items.ToList());
            AddDomainEvent(saleCompletedEvent);
        }

        public void CancelSale()
        {
            if(Status == SaleStatus.Cancelled)
                throw new DomainException("Sale has already been cancelled.");

            if (Status == SaleStatus.Completed)
                throw new DomainException("Cannot cancel a sale that has already been completed.");

            Status = SaleStatus.Cancelled;
        }

        private void RecalculateTotals()
        {
            TotalPrice = Items.Sum(item => item.PriceAtSale * item.Quantity);
            FinalPrice = TotalPrice - Discount;
        }
    }
}
