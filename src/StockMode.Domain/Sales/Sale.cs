using StockMode.Domain.Core.Model;
using StockMode.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Domain.Sales
{
    public class Sale : Entity<Sale>
    {
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public SaleStatus Status { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
