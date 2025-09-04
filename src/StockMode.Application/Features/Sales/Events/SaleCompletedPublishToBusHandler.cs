using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using StockMode.Application.Features.Sales.Commands.SendSaleConfirmationEmail;
using StockMode.Domain.Sales.Events;

namespace StockMode.Application.Features.Sales.Events
{
    public class SaleCompletedPublishToBusHandler(IMediator mediator) : INotificationHandler<SaleCompletedEvent>
    {
        public async Task Handle(SaleCompletedEvent notification, CancellationToken cancellationToken)
        {
            var command = new SendSaleConfirmationEmailCommand(notification.SaleId, "igorsantanamedeiros17@gmail.com");

            var messageWrapper = new QueueMessageWrapper
            {
                MessageType = command.GetType().AssemblyQualifiedName ?? throw new InvalidOperationException("Command type cannot be null"),
                Payload = System.Text.Json.JsonSerializer.Serialize(command)
            };
            await mediator.Send(command);
        }
    }
}
