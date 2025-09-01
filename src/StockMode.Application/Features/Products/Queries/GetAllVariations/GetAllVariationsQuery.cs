using MediatR;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Products.Queries.GetAllVariations
{
    public record GetAllVariationsQuery(string? name, string? sku, int Page, int PageSize) : IRequest<PagedResult<VariationDetailDto>>;
}
