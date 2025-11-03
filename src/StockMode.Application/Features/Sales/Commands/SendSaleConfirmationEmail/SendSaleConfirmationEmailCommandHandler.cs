using EasyNetQ;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockMode.Application.Common.Dtos;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using StockMode.Application.Exceptionns;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Application.PDF;
using StockMode.Domain.Products;
using StockMode.Domain.Sales;

namespace StockMode.Application.Features.Sales.Commands.SendSaleConfirmationEmail
{
    public class SendSaleConfirmationEmailCommandHandler(
        IServiceProvider service, 
        IBus bus, 
        IMessageDeliveryReporter reporter,
       ILogger<SendSaleConfirmationEmailCommandHandler> logger, IPdfMaker pdfMaker) : IRequestHandler<SendSaleConfirmationEmailCommand>
    {
        public async Task Handle(SendSaleConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var scope = service.CreateScope();
                var sender = scope.ServiceProvider.GetRequiredService<IMailer>();
                var saleRepository = scope.ServiceProvider.GetRequiredService<ISaleRepository>();
                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

                Sale? sale = await saleRepository.GetSaleByIdAsync(request.SaleId, cancellationToken);

                if (sale is null)
                    throw new NotFoundException(nameof(Sale), request.SaleId);

                var varationIds = sale?.Items.Select(i => i.VariationId).Distinct().ToList();
                var variations = await productRepository.GetVariationsWithProductsByIdsAsync(varationIds!);
                var variationNames = variations.ToDictionary(v => v.Id, v => $"{v.Product.Name} - {v.Name}");

                var mailData = new SaleCompletedEmail(
                    sale!.Id,
                    request.Email,
                    sale.TotalPrice,
                    sale.Discount,
                    sale.FinalPrice,
                    sale.PaymentMethod,
                    sale.SaleDate,
                    sale.Items.Select(i => new SaleItemDetailsDto(
                        i.Id,
                        variationNames.GetValueOrDefault(i.VariationId, "Unknown Product"),
                        i.VariationId,
                        i.Quantity,
                        i.PriceAtSale
                    )).ToList());
                
                var pdfBytes = pdfMaker.CreatePdfGeneric(mailData, SaleCompletedRenderer.DrawSaleCompletedDetails);
                var pdfFileName = $"SaleReceipt_{sale.Id}.pdf";

                var attachments = new List<EmailAttachment>
                {
                    new EmailAttachment(pdfFileName, pdfBytes, "application/pdf")
                };

                var emailBody = new EmailMessage<SaleCompletedEmail>(
                    request.Email,
                    $"New Sale Completed for email: {request.Email}",
                    "SaleCompleted",
                    mailData,
                    attachments
                    );

                var messageQueue = scope.ServiceProvider.GetRequiredService<IMessageQueue>();
                await messageQueue.PublishEmailAsync(emailBody, cancellationToken);
                
                logger.LogInformation("Sale confirmation email queued for {Email}", request.Email);
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
                await reporter.ReportAsync(report);
                throw;
            }
        }
    }
}
