using MediatR;
using StockMode.Application.Common.Dtos;

namespace StockMode.Application.Features.Customers.Commands.UpdateCustomer
{
    public record UpdateCustomerCommand(int CustomerId,
                                        string Name,
                                        string Email,
                                        string PhoneNumber,
                                        AddressDto AddressDto, string? Notes, IEnumerable<int>? TagIds) : IRequest;
}
