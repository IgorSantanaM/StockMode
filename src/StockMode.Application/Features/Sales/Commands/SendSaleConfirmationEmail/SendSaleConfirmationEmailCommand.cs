using MediatR;

namespace StockMode.Application.Features.Sales.Commands.SendSaleConfirmationEmail
{
    public record SendSaleConfirmationEmailCommand(int SaleId, string Email) : IRequest;
}
