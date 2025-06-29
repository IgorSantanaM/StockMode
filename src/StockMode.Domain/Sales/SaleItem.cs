using StockMode.Domain.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMode.Domain.Core.Model;

namespace StockMode.Domain.Sales
{
    public class SaleItem : Entity<SaleItem>
    {
        [Required]
        public int SaleId { get; set; }

        [Required]
        public int VariationId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PriceAtSale { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }

        [ForeignKey("VariationId")]
        public Variation Variation { get; set; }
    }
}
