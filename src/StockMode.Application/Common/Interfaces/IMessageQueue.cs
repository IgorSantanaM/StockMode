using MediatR;
using StockMode.Application.Common.Messaging;

namespace StockMode.Application.Common.Interfaces
{
    public interface IMessageQueue
    {
        // RabbitMQ-based email publishing methods
        Task PublishEmailAsync<TModel>(EmailMessage<TModel> emailMessage, CancellationToken cancellationToken = default);
        Task PublishEmailAsync(GenericEmailMessage emailMessage, CancellationToken cancellationToken = default);
    }
}
