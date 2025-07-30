using EasyNetQ;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using StockMode.Application.Exceptionns;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Sales;

namespace StockMode.Application.Features.Sales.Commands.SendSaleConfirmationEmail
{
    public class SendSaleConfirmationEmailCommandHandler(IServiceProvider service, IBus bus, IMessageDeliveryReporter reporter) : IRequestHandler<SendSaleConfirmationEmailCommand>
    {
        public async Task Handle(SendSaleConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var scope = service.CreateScope();
                var sender = scope.ServiceProvider.GetRequiredService<IMailer>();
                var repository = scope.ServiceProvider.GetRequiredService<ISaleRepository>();
                Sale? sale = await repository.GetSaleByIdAsync(request.SaleId, cancellationToken);

                if (sale is null)
                    throw new NotFoundException(nameof(Sale), request.SaleId);

                var mailData = new SaleCompletedEmail(
                    sale.Id,
                    request.Email,
                    sale.FinalPrice,
                    sale.PaymentMethod,
                    sale.SaleDate,
                    sale.Items.Select(i => new SaleItemDetailsDto(
                        i.Id,
                        i.VariationId,
                        i.Quantity,
                        i.PriceAtSale
                    )).ToList());

                await bus.PubSub.PublishAsync(mailData, cancellationToken);
            }
            catch (Exception ex)
            {
                var report = new DeliveryReport
                {
                    MessageId = Guid.NewGuid(),
                    Recipient = request.Email,
                    Status = DeliveryStatus.Failure,
                    ErrorDetails = ex.Message,
                    Metadata = new Dictionary<string, object>
                    {
                        { "SaleId", request.SaleId },
                        { "Email", request.Email },
                        { "Timestamp", DateTime.UtcNow }
                    }
                };
                await reporter.ReportReportAsync(report);
                throw;
            }
        }
    }
}
