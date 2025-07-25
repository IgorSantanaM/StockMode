namespace StockMode.Application.Common.Interfaces
{
    public interface IMessageQueue
    {
        Task AddMessageToQueue<T>(QueueMessage<T> eventData);
        Task<QueueMessage<T>?> FetchFromQueueAsync<T>(string queueName, CancellationToken token);
    }
}
