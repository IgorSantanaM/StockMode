using MediatR;
using StockMode.Application.Features.Customers.Dtos;

namespace StockMode.Application.Features.Customers.Queries.GetAllCustomers
{
    public record GetAllCustomersQuery() : IRequest<IEnumerable<CustomerSummaryDto>>;
}
