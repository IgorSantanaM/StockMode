using MediatR;
using StockMode.Application.Features.Customers.Dtos;
using StockMode.Domain.Common;

namespace StockMode.Application.Features.Customers.Queries.GetAllCustomers
{
    public record GetAllCustomersQuery(string? Name, int Page, int PageSize) : IRequest<PagedResult<CustomerSummaryDto>>;
}
