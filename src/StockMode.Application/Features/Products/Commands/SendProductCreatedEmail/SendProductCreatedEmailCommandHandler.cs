using Dapper;
using EasyNetQ;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MimeKit.Cryptography;
using RabbitMQ.Client.Logging;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using StockMode.Application.Exceptionns;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Domain.Customers;
using StockMode.Domain.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Products.Commands.SendProductCreatedEmail
{
    public class SendProductCreatedEmailCommandHandler : IRequestHandler<SendProductCreatedEmailCommand>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMailer _mailer;
        private readonly IBus _bus;
        private readonly IMessageDeliveryReporter _reporter;

        public SendProductCreatedEmailCommandHandler(IServiceProvider serviceProvider, IBus bus, IMessageDeliveryReporter reporter, IDbConnection dbConnection, IMailer mailer)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
            _reporter = reporter;
            _mailer = mailer;
        }

        public async Task Handle(SendProductCreatedEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var sender = scope.ServiceProvider.GetRequiredService<IMailer>();
                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                var dbConnection = scope.ServiceProvider.GetRequiredService<IDbConnection>();

                var getAllCustomersEmails = @"SELECT ""Email"" FROM ""Customers"" as c LEFT JOIN ""Sales"" as s ON c.""Id"" = s.""CustomerId"" where s.""FinalPrice"" <> 0";

                var customerEmails = (await dbConnection.QueryAsync<string>(getAllCustomersEmails)).Distinct().ToList();

                if (!customerEmails.Any()) return; 
    
                var productCreatedEmail  = new ProductCreatedEmail(request.ProductCreatedEvent.Name!, request.ProductCreatedEvent.Description!, request.ProductCreatedEvent.Variations.Select(v => new VariationsForEmailSendingDto(v.Name, v.Sku, v.SalePrice)).ToList(), customerEmails);

                var emailBody  = new EmailMessage<ProductCreatedEmail>(
                    string.Join(",", customerEmails),
                    $"New Product Added: {request.ProductCreatedEvent.Name}",
                    "ProductCreated",
                    productCreatedEmail,
                    null); 

                var messageQueue = scope.ServiceProvider.GetRequiredService<IMessageQueue>();
                await messageQueue.PublishEmailAsync(emailBody, cancellationToken);

            }catch(Exception ex)
            {
                var report = new DeliveryReport
                {
                    MessageId = Guid.NewGuid(),
                    Recipient = "Multiple Emails Target!",
                    Status = DeliveryStatus.Failure,
                    ErrorDetails = ex.Message,
                    Metadata = new Dictionary<string, object>
                    {
                        { "ProductId", request.ProductCreatedEvent.ProductId },
                        { "Timestamp", DateTime.UtcNow }
                    }
                };

                await _reporter.ReportAsync(report);
                return;
            }
        }
    }
}
