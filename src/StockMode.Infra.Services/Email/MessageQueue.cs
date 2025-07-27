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
        public MessageQueue(int capacity = 40)
        {
            var options = new BoundedChannelOptions(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait,
            };
            channel = Channel.CreateBounded<QueueMessageWrapper>(options);
        }

        public async Task AddMessageToQueue(string queueName, QueueMessageWrapper queueMessageWrapper)
            => await channel.Writer.WriteAsync(queueMessageWrapper);

        public async Task<QueueMessageWrapper?> FetchFromQueueAsync(string queueName, CancellationToken token)
            => await channel.Reader.ReadAsync(token);
    }
}
