using EasyNetQ;
using MediatR;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using StockMode.Application.Features.Products.Commands.SendProductCreatedEmail;
using StockMode.Domain.Products.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Products.Events
{
    public class ProductCreatedPublishToBusHandler(IMediator mediator) : INotificationHandler<ProductCreatedEvent>
    {
        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            var command = new SendProductCreatedEmailCommand(notification);

            var messageWrapper = new QueueMessageWrapper(
                 command.GetType().AssemblyQualifiedName ?? throw new InvalidOperationException("Command type cannot be null"),
                 System.Text.Json.JsonSerializer.Serialize(command));

            await mediator.Send(command, cancellationToken);
        }
    }
}
