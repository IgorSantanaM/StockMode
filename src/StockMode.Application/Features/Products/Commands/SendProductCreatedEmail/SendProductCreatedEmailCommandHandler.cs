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
        private readonly IBus _bus;
        private readonly IMessageDeliveryReporter _reporter;

        public SendProductCreatedEmailCommandHandler(IServiceProvider serviceProvider, IBus bus, IMessageDeliveryReporter reporter, IDbConnection dbConnection)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
            _reporter = reporter;
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

                Product? product = await productRepository.GetProductByIdAsync(request.ProductId, cancellationToken);

                if (product is null)
                    throw new NotFoundException(nameof(Customer), request.ProductId);

                    var getAllCsutomerEmailsQuery = @"SELECT ""Email"" FROM ""Customers"" as c LEFT JOIN ""Sales"" as s ON c.""Id"" = s.""CustomerId"" where s.""FinalPrice"" <> 0";

                var customerEmails = (await dbConnection.QueryAsync<string>(getAllCsutomerEmailsQuery)).Distinct().ToList();

                if (!customerEmails.Any()) return; 
 
                var productCreatedEmail  = new ProductCreatedEmail(product.Name, product.Description!, product.Variations.Select(v => new VariationDetailDto(v.Id, v.Name, v.Sku, v.CostPrice, v.SalePrice, v.StockQuantity)).ToList(), customerEmails);

                await _bus.PubSub.PublishAsync(productCreatedEmail, cancellationToken);
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
                        { "ProductId", request.ProductId },
                        { "Timestamp", DateTime.UtcNow }
                    }
                };

                await _reporter.ReportAsync(report);
                return;
            }
        }
    }
}
