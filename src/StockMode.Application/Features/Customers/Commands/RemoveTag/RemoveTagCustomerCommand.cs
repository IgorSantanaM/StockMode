using MediatR;

namespace StockMode.Application.Features.Suppliers.Commands.RemoveTag;

public record RemoveTagCustomerCommand(int CustomerId, int TagId) : IRequest;

