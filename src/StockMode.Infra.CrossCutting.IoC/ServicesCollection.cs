using EasyNetQ;
using FluentValidation;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mjml.Net;
using Npgsql;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Features.Products.Commands.CreateProduct;
using StockMode.Application.Features.Products.Validators;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Customers;
using StockMode.Domain.Products;
using StockMode.Domain.Sales;
using StockMode.Domain.StockMovements;
using StockMode.Domain.Suppliers;
using StockMode.Domain.Tags;
using StockMode.EmailWorker;
using StockMode.Infra.Data.Contexts;
using StockMode.Infra.Data.Repositories;
using StockMode.Infra.Data.UoW;
using StockMode.Infra.Services.Email;
using StockMode.Infra.Services.PDF;
using System.Data;
using System.Reflection;
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
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
        }

        public static void AddMailServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jsonOptions = new JsonSerializerOptions();

            // Get RabbitMQ connection string from configuration
            var rabbitMqConnectionString = configuration.GetConnectionString("RabbitMQ") 
                ?? "host=localhost;username=guest;password=guest;virtualHost=/";

            IBus? bus = RabbitHutch.CreateBus(rabbitMqConnectionString, options =>
            options.EnableNewtonsoftJson());

            services.AddSingleton(bus);

            var smtpSettings = new SmtpSettings();
            configuration.GetSection("SmtpSettings").Bind(smtpSettings);

            services.AddSingleton(smtpSettings);

            services.AddTransient<IMessageDeliveryReporter, SignalRDeliveryReporter>();
            services.AddScoped<IMessageQueue, MessageQueue>();
            services.AddScoped<IMailSender, SmptMailSender>();
            services.AddScoped<IMailer, Mailer>();

            services.AddSingleton<IMailTemplateProvider, EmbeddedResourceMailTemplateProvider>();
            services.AddSingleton<IMjmlRenderer>(_ => new MjmlRenderer());
            services.AddSingleton<IHtmlMailRenderer, RazorLightMjmlMailRenderer>();
            services.AddSingleton<IPdfMaker, PdfMaker>();
        }

        public static void ConfigureOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            var resourceBuilder = ResourceBuilder.CreateDefault()
                .AddService("StockMode.WebApi",
                    serviceNamespace: "StockMode.OpenTelemetry",
                    serviceVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0")
                .AddTelemetrySdk();

            services.AddOpenTelemetry()
                .WithTracing(tracerProviderBuilder =>
                {
                    tracerProviderBuilder
                        .SetResourceBuilder(resourceBuilder)
                        .AddSource("StockMode.WebApi")
                        .AddAspNetCoreInstrumentation(options =>
                        {
                            options.Filter = (httpContext) =>
                            {
                                return !httpContext.Request.Path.Value?.Contains("swagger") ?? true;
                            };
                        })
                        .AddHttpClientInstrumentation()
                        .AddNpgsql();
                    var otlpEndpoint = "http://jaeger:4318";

                    if (!string.IsNullOrEmpty(otlpEndpoint))
                    {
                        tracerProviderBuilder.AddOtlpExporter(opt =>
                        {
                            opt.Endpoint = new Uri(otlpEndpoint);
                            opt.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                        });
                    }
                    else
                    {
                        tracerProviderBuilder.AddConsoleExporter();
                    }
                });
        }
    }
}
