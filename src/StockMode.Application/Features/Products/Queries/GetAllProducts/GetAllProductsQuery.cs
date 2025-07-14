using MediatR;
using StockMode.Application.Features.Products.Dtos;

namespace StockMode.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery() : IRequest<IEnumerable<ProductSummaryDto>>;
}
