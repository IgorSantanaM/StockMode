using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockMode.Application.Common.Interfaces;
using StockMode.EmailWorker;
using StockMode.Infra.CrossCutting.IoC;
using StockMode.Infra.Services.Email;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var smtpSettings = hostContext.Configuration.GetSection("SmtpSettings");
        services.AddSingleton(smtpSettings);
        services.AddMailServices();
        services.AddTransient<IMailSender, SmptMailSender>();
        services.AddTransient<IMailer, Mailer>();

        services.AddHostedService<MailSenderHostedService>();
    }).Build().RunAsync();