using MediatR;
using StockMode.Application.Features.Customers.Dtos;

namespace StockMode.Application.Features.Customers.Commands.UpdateCustomer
{
    public record UpdateCustomerCommand(int CustomerId,
                                        string Name,
                                        string Email,
                                        string PhoneNumber,
                                        AddressDto AddressDto) : IRequest;
}
