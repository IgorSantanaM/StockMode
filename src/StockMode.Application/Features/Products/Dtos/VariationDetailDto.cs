using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Products.Dtos
{
    public record VariationDetailDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Sku { get; init; }
        public decimal CostPrice { get; init; }
        public decimal SalePrice { get; init; }
        public int StockQuantity { get; init; }
    }
}
