using EasyNetQ;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mjml.Net;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Features.Products.Commands.CreateProduct;
using StockMode.Application.Features.Products.Validators;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Products;
using StockMode.Domain.Sales;
using StockMode.Domain.StockMovements;
using StockMode.EmailWorker;
using StockMode.Infra.Data.Contexts;
using StockMode.Infra.Data.Repositories;
using StockMode.Infra.Data.UoW;
using StockMode.Infra.Services.Email;
using System.Data;
using System.Text.Json;

namespace StockMode.Infra.CrossCutting.IoC
{
    public static class ServicesCollection
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Application
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly)
            );

            services.AddScoped<IDbConnection>(sp =>
                sp.GetRequiredService<StockModeContext>().Database.GetDbConnection());

            services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);

            // Infra - Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IStockMovementRepository, StockMovementRepository>();
        }

        public static void AddMailServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jsonOptions = new JsonSerializerOptions();

            IBus? bus = RabbitHutch.CreateBus("host=mailrabbit;username=guest;password=guest", options => 
            options.EnableNewtonsoftJson());

            services.AddSingleton(bus);

            var smtpSettings = new SmtpSettings();
            configuration.GetSection("SmtpSettings").Bind(smtpSettings);

            services.AddSingleton(smtpSettings);

            services.AddTransient<IMessageDeliveryReporter, SignalRDeliveryReporter>();
            services.AddScoped<IMessageQueue, MessageQueue>();
            services.AddScoped<IMailSender, SmptMailSender>();

            services.AddSingleton<IMailTemplateProvider, EmbeddedResourceMailTemplateProvider>();
            services.AddSingleton<IMjmlRenderer>(_ => new MjmlRenderer());
            services.AddSingleton<IHtmlMailRenderer, RazorLightMjmlMailRenderer>();
        }
    }
}
