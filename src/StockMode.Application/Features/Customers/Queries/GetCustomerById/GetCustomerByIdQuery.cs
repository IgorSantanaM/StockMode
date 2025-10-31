using MediatR;
using StockMode.Application.Features.Customers.Dtos;

namespace StockMode.Application.Features.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(int CustomerId) : IRequest<CustomerDetailsDto>;


