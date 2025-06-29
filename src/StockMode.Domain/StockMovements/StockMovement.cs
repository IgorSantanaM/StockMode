using StockMode.Domain.Core.Model;
using StockMode.Domain.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMode.Domain.Enums;

namespace StockMode.Domain.StockMovements
{
    public class StockMovement : Entity<StockMovement>
    {
        [Required]
        public int VariationId { get; set; }

        public int? SaleId { get; set; } 

        [Required]
        public StockMovementType Type { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int StockAfterMovement { get; set; }

        public string? Note { get; set; }

        public DateTime MovementDate { get; set; } = DateTime.Now;

        [ForeignKey("VariationId")]
        public Variation Variation { get; set; }
    }
}
