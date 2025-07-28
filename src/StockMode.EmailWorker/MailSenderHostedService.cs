using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Sales.Events;
using System.Data.Common;

namespace StockMode.EmailWorker
{
    public class MailSenderHostedService(IBus bus,
        IMailer mailer, ILogger<MailSenderHostedService> logger) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        => await bus.PubSub.SubscribeAsync<SaleCompletedEmail>("email-queue", async data => await SendMailAsync(data, cancellationToken));

        public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

        private async Task SendMailAsync(SaleCompletedEmail saleCompletedEmail, CancellationToken cancellationToken)
        {
            try
            {
                await mailer.SendSaleCompletedAsync(saleCompletedEmail, cancellationToken);
                await bus.PubSub.PublishAsync(new DeliveryReport
                {
                    MessageId = Guid.NewGuid(),
                    Recipient = saleCompletedEmail.Email,
                    Status = DeliveryStatus.Success,
                    Metadata = new Dictionary<string, object>
                    {
                        { "SaleId", saleCompletedEmail.SaleId },
                        { "Email", saleCompletedEmail.Email },
                        { "Timestamp", DateTime.UtcNow }
                    }
                }, cancellationToken);
                logger.LogInformation("Email sent successfully for Sale ID: {SaleId}", saleCompletedEmail.SaleId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send email for Sale ID: {SaleId}", saleCompletedEmail.SaleId);
                await bus.PubSub.PublishAsync(new DeliveryReport
                {
                    MessageId = Guid.NewGuid(),
                    Recipient = saleCompletedEmail.Email,
                    Status = DeliveryStatus.Failure,
                    Metadata = new Dictionary<string, object>
                    {
                        { "SaleId", saleCompletedEmail.SaleId },
                        { "Email", saleCompletedEmail.Email },
                        { "Timestamp", DateTime.UtcNow }
                    }
                }, cancellationToken);
            }
        }
    }
}
