namespace StockMode.Application.Common.Interfaces;

public record QueueMessage<T>(string QueueName, T Body);

