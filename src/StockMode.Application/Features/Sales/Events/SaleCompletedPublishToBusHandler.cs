using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using StockMode.Application.Features.Sales.Commands.SendSaleConfirmationEmail;
using StockMode.Domain.Sales.Events;
using System.Data;

namespace StockMode.Application.Features.Sales.Events
{
    public class SaleCompletedPublishToBusHandler(IMediator mediator, IDbConnection dbConnection, ILogger<SaleCompletedPublishToBusHandler> logger) : INotificationHandler<SaleCompletedEvent>
    {
        public async Task Handle(SaleCompletedEvent notification, CancellationToken cancellationToken)
        {
            if(notification.CustomerId <= 0)
            {
                logger.LogInformation("Sale was not associated to a customer");
                return;
            }

            var query = @"SELECT ""Email"" FROM ""Customers"" WHERE ""Id"" = @CustomerId";

            var email = await dbConnection.QueryFirstOrDefaultAsync<string>(query, new { CustomerId = notification.CustomerId});

            if (string.IsNullOrEmpty(email))
            {
                logger.LogWarning("Customer doesn't have an email.");
                return;
            }

            var command = new SendSaleConfirmationEmailCommand(notification.SaleId, email);

            var messageWrapper = new QueueMessageWrapper(
                command.GetType().AssemblyQualifiedName ?? throw new InvalidOperationException("Command type cannot be null"),
                 System.Text.Json.JsonSerializer.Serialize(command));

            await mediator.Send(command);
        }
    }
}
