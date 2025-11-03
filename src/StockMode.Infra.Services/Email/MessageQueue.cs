using EasyNetQ;
using Microsoft.Extensions.Logging;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace StockMode.Infra.Services.Email
{
    public class MessageQueue : IMessageQueue
    {
        private readonly Channel<QueueMessageWrapper> channel;
        private readonly IBus _bus;
        private readonly ILogger<MessageQueue> _logger;

        public MessageQueue(IBus bus, ILogger<MessageQueue> logger, int capacity = 40)
        {
            var options = new BoundedChannelOptions(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait,
            };
            channel = Channel.CreateBounded<QueueMessageWrapper>(options);
            _bus = bus;
            _logger = logger;
        }

        public async Task PublishEmailAsync<TModel>(EmailMessage<TModel> emailMessage, CancellationToken cancellationToken = default)
        {
            try
            {
                var genericMessage = new GenericEmailMessage(
                    emailMessage.To,
                    emailMessage.Subject,
                    emailMessage.TemplateName,
                    emailMessage.Model!,
                    emailMessage.attachments
                );

                await _bus.PubSub.PublishAsync(genericMessage, cancellationToken);
                _logger.LogInformation("Published email message to queue for {To} with template {Template}", 
                    emailMessage.To, emailMessage.TemplateName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing email message to queue for {To}", emailMessage.To);
                throw;
            }
        }

        public async Task PublishEmailAsync(GenericEmailMessage emailMessage, CancellationToken cancellationToken = default)
        {
            try
            {
                await _bus.PubSub.PublishAsync(emailMessage, cancellationToken);
                _logger.LogInformation("Published generic email message to queue for {To} with template {Template}", 
                    emailMessage.To, emailMessage.TemplateName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing generic email message to queue for {To}", emailMessage.To);
                throw;
            }
        }
    }
}
