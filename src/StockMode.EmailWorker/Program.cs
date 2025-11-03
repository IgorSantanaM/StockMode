using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockMode.Application.Common.Interfaces;
using StockMode.EmailWorker;
using StockMode.Infra.CrossCutting.IoC;
using StockMode.Infra.Services.Email;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // Enable the hosted service to process emails from RabbitMQ
        services.AddHostedService<MailSenderHostedService>();
        
        // Add mail services (RabbitMQ, SMTP, etc.)
        services.AddMailServices(hostContext.Configuration);
        services.AddTransient<IMailSender, SmptMailSender>();
        services.AddTransient<IMailer, Mailer>();
        

    }).Build().RunAsync();