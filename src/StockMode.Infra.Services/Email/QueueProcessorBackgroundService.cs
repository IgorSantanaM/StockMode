using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockMode.Application.Common.Interfaces;
using System.Text.Json;

namespace StockMode.Infra.Services.Email
{
    public class QueueProcessorBackgroundService(IMessageQueue queue,
        IServiceProvider service,
        ILogger<QueueProcessorBackgroundService> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            => await ProcessMessageQueue(stoppingToken);

        private async Task ProcessMessageQueue(CancellationToken stoppingToken)
        {
            logger.LogInformation("Queue processor is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var messageWrappper = await queue.FetchFromQueueAsync("email-queue", stoppingToken);
                if (messageWrappper is null)
                {
                    await Task.Delay(1000, stoppingToken);
                    continue;
                }

                try
                {
                    var messageType = Type.GetType(messageWrappper.MessageType);
                    if (messageType == null)
                    {
                        logger.LogWarning("Message type {MessageType} not found.", messageWrappper.MessageType);
                        continue;
                    }

                    var command = JsonSerializer.Deserialize(messageWrappper.Payload, messageType);
                    if (command is null)
                    {
                        logger.LogWarning("Failed to deserialize message payload for type {MessageType}.", messageWrappper.MessageType);
                        continue;
                    }

                    using (var scope = service.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        logger.LogInformation("Processing message of type {MessageType}.", messageWrappper.MessageType);
                        await mediator.Send(command, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error processing message of type {MessageType}.", messageWrappper.MessageType);
                }
            }
        }
    }
}
