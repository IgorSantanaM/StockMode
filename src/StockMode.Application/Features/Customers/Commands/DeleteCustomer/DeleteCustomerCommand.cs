using MediatR;

namespace StockMode.Application.Features.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(int CustomerId) : IRequest;
