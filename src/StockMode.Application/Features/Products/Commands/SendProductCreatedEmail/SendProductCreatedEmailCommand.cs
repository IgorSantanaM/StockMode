using MediatR;

namespace StockMode.Application.Features.Products.Commands.SendProductCreatedEmail
{
    public record SendProductCreatedEmailCommand(int ProductId) : IRequest;
}
