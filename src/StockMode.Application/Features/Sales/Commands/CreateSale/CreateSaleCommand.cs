using MediatR;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Enums;

namespace StockMode.Application.Features.Sales.Commands.CreateSale
{
    public record CreateSaleCommand(PaymentMethod PaymentMethod) : IRequest<int>;
}
