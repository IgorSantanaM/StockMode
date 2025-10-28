﻿using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Sales.Events;
using System.Data.Common;
using System.Text.Json;
using System.Threading;

namespace StockMode.EmailWorker
{
    /// <summary>
    /// Flexible email worker that can handle any email template type via GenericEmailMessage
    /// </summary>
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

                    // Subscribe to generic email messages - handles ALL template types
                    await bus.PubSub.SubscribeAsync<GenericEmailMessage>(
                        "generic-email-queue", 
                        async data => await SendGenericEmailAsync(data, stoppingToken), 
                        stoppingToken);

                    logger.LogInformation("Subscribed to GenericEmailMessage events. Ready to process any email template.");
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while connecting to message bus. Retrying in 5 seconds...");
                    await Task.Delay(5000, stoppingToken);
                }
            }

            // Keep service alive
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        /// <summary>
        /// Sends email from a generic message wrapper - works with any template
        /// </summary>
        private async Task SendGenericEmailAsync(GenericEmailMessage genericMessage, CancellationToken cancellationToken)
        {
            using var scope = service.CreateScope();
            var mailer = scope.ServiceProvider.GetRequiredService<IMailer>();
            var bus = scope.ServiceProvider.GetRequiredService<IBus>();

            try
            {
                logger.LogInformation(
                    "Processing email for {To} with template {Template}", 
                    genericMessage.To, 
                    genericMessage.TemplateName);

                // Send email using the generic renderer that works with any model type
                await mailer.SendGenericAsync(
                    genericMessage.To,
                    genericMessage.Subject,
                    genericMessage.TemplateName,
                    genericMessage.ModelJson,
                    cancellationToken);

                // Report success
                await bus.PubSub.PublishAsync(new DeliveryReport
                {
                    MessageId = Guid.NewGuid(),
                    Recipient = genericMessage.To,
                    Status = DeliveryStatus.Success,
                    Metadata = new Dictionary<string, object>
                    {
                        { "Email", genericMessage.To },
                        { "Template", genericMessage.TemplateName },
                        { "Timestamp", DateTime.UtcNow }
                    }
                }, cancellationToken);

                logger.LogInformation(
                    "Successfully sent email to {To} using template {Template}", 
                    genericMessage.To, 
                    genericMessage.TemplateName);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex, 
                    "Failed to send email to {To} using template {Template}", 
                    genericMessage.To, 
                    genericMessage.TemplateName);

                // Report failure
                await bus.PubSub.PublishAsync(new DeliveryReport
                {
                    MessageId = Guid.NewGuid(),
                    Recipient = genericMessage.To,
                    Status = DeliveryStatus.Failure,
                    ErrorDetails = ex.Message,
                    Metadata = new Dictionary<string, object>
                    {
                        { "Email", genericMessage.To },
                        { "Template", genericMessage.TemplateName },
                        { "Timestamp", DateTime.UtcNow },
                        { "Error", ex.Message }
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Legacy method - kept for backwards compatibility with strongly typed messages
        /// </summary>
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
