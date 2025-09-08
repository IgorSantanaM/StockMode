using MediatR;
using StockMode.Domain.Products.Events;

namespace StockMode.Application.Features.Products.Commands.SendProductCreatedEmail
{
    public record SendProductCreatedEmailCommand(ProductCreatedEvent ProductCreatedEvent) : IRequest;
}
