using EasyNetQ;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using StockMode.Domain.Sales;

namespace StockMode.Application.Features.Sales.Commands.SendSaleConfirmationEmail
{
    public class SendSaleConfirmationEmailCommandHandler(ISaleRepository saleRepository, IServiceProvider service, IBus bus, IMessageDeliveryReporter reporter) : IRequestHandler<SendSaleConfirmationEmailCommand>
    {
        public async Task Handle(SendSaleConfirmationEmailCommand request, CancellationToken cancellationToken)
        {

            using var scope = service.CreateScope();
            var sender = scope.ServiceProvider.GetRequiredService<IMailer>();
            try
            {
                var sale = await saleRepository.GetSaleByIdAsync(request.SaleId, cancellationToken);

            }
            catch (Exception ex)
            {
                //reporter.ReportReportAsync(DeliveryReport.);
                //throw;

            }
        }
    }
}
