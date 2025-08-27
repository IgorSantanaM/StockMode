using MediatR;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Common;
using StockMode.Domain.Enums;

namespace StockMode.Application.Features.Sales.Queries.GetAllSales
{
    public record GetAllSalesQuery(DateTime? StartDate,
    DateTime? EndDate, SaleStatus? Status, int Page, int PageSize) : IRequest<PagedResult<SaleSummaryDto>>;
}
