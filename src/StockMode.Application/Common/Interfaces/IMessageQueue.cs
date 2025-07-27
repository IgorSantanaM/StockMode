using MediatR;
using StockMode.Application.Common.Messaging;

namespace StockMode.Application.Common.Interfaces
{
    public interface IMessageQueue
    {
        Task AddMessageToQueue(string queueName, QueueMessageWrapper queueMessageWrapper);
        Task<QueueMessageWrapper?> FetchFromQueueAsync(string queueName, CancellationToken token);
    }
}
