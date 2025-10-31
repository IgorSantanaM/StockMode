using MediatR;
using StockMode.Application.Common.Dtos;

namespace StockMode.Application.Features.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(string Name, string Email, string PhoneNumber, AddressDto AddressDto, IReadOnlyCollection<TagIdDto>? TagIds, string? Notes) : IRequest<int>;
