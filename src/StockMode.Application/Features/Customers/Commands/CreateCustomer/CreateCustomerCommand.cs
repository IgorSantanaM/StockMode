using MediatR;
using StockMode.Application.Features.Customers.Dtos;

namespace StockMode.Application.Features.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(string Name, string Email, string PhoneNumber, AddressDto AddressDto) : IRequest<int>;
