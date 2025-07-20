using MediatR;
using StockMode.Application.Features.Sales.Dtos;

namespace StockMode.Application.Features.Sales.Queries.GetAllSales
{
    public record GetAllSalesQuery : IRequest<IEnumerable<SaleSummaryDto>>;
}
