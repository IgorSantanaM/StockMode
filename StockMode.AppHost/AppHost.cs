using Aspire.Hosting;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var rabbitMq = builder.AddRabbitMQ("rabbitmq");

var rabbitMqEndpoint = rabbitMq.GetEndpoint("amqp");

var stockmodeDb = builder.AddPostgres("postgres")
    .AddDatabase("stockmodedb");

var idsrv = builder.AddProject<StockMode_IDP>("stockmode-idp");
var idEndpoint = idsrv.GetEndpoint("https");

var api = builder.AddProject<StockMode_WebApi>("stockmode-webapi")
    .WithEnvironment("Auth__Authority", idEndpoint)
    .WithReference(rabbitMq)
    .WithReference(stockmodeDb);

var emailWorker = builder.AddProject<StockMode_EmailWorker>("stockmode-emailworker")
    .WithEnvironment("ConnectionStrings__RabbitMQ", rabbitMqEndpoint);

builder.Build().Run();
