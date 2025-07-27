using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockMode.EmailWorker;

//await Host.CreateDefaultBuilder(args)
//    .ConfigureServices((hostContext, services) =>
//    {
//        var smtpSettings = hostContext.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>() ?? new SmtpSettings();
//        services.AddSingleton(smtpSettings);
//    })