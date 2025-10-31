using MediatR;

namespace StockMode.Application.Features.Suppliers.Commands.AddTag;

public record AddTagCustomerCommand(int CustomerId, int TagId) : IRequest;
