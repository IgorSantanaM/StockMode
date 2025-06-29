using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Core.Model;
using StockMode.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Domain.Sales
{
    public class Sale : Entity<Sale>, IAggregateRoot
    {
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public SaleStatus Status { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;   
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

        public void AddItem(SaleItem item)
        {
            if (this.Status == SaleStatus.Completed)
                throw new DomainException("It is not possible to add items to a completed sale.");

        }
    }
}
