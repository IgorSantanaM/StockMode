using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Sales.Events;
using System.Data.Common;
using System.Threading;

namespace StockMode.EmailWorker
{
    public class MailSenderHostedService(IServiceProvider service, ILogger<MailSenderHostedService> logger) : BackgroundService
    {
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = service.CreateScope();
                    var bus = scope.ServiceProvider.GetRequiredService<IBus>();
                    logger.LogInformation("Connecting to message bus...");

                    await bus.PubSub.SubscribeAsync<SaleCompletedEmail>("email-queue", async data => await SendMailAsync(data, stoppingToken), stoppingToken);

                    logger.LogInformation("Subscribed to SaleCompletedEmail events.");
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while connecting to message bus. Retrying in 5 seconds...");
                    await Task.Delay(5000, stoppingToken);
                }
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task SendMailAsync<TModel>(EmailMessage<TModel> emailMessage, CancellationToken cancellationToken)
        {
            using var scope = service.CreateScope();
            var mailer = scope.ServiceProvider.GetRequiredService<IMailer>();
            var bus = scope.ServiceProvider.GetRequiredService<IBus>();
            try
            {
                await mailer.SendAsync(emailMessage, cancellationToken);
                await bus.PubSub.PublishAsync(new DeliveryReport
                {
                    MessageId = Guid.NewGuid(),
                    Recipient = emailMessage.To,
                    Status = DeliveryStatus.Success,
                    Metadata = new Dictionary<string, object>
                    {
                        { "Email", emailMessage.To },
                        { "Timestamp", DateTime.UtcNow }
                    }
                }, cancellationToken);
            }
            catch (Exception ex)
            {
                await bus.PubSub.PublishAsync(new DeliveryReport
                {
                    MessageId = Guid.NewGuid(),
                    Recipient = emailMessage.To,
                    Status = DeliveryStatus.Failure,
                    Metadata = new Dictionary<string, object>
                    {
                        { "Email", emailMessage.To },
                        { "Timestamp", DateTime.UtcNow }
                    }
                }, cancellationToken);
            }
        }
    }
}
