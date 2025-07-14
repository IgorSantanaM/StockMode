using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Products.Dtos
{
    public record ProductDetailsDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public bool IsActive { get; init; }
        public IReadOnlyCollection<VariationDetailDto> Variations { get; init; } = new List<VariationDetailDto>();
    }
}
