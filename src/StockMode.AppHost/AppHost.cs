using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var rabbitMq = builder.AddRabbitMQ("rabbitmq");

var stockmodeDb = builder.AddPostgres("postgres")
    .AddDatabase("stockmodedb");

var idsrv = builder.AddProject<StockMode_IDP>("stockmode-idp");
var idEndpoint = idsrv.GetEndpoint("https");

var api = builder.AddProject<StockMode_WebApi>("stockmode-webapi")
    .WithEnvironment("Auth__Authority", idEndpoint)
    .WithReference(rabbitMq)
    .WithReference(stockmodeDb)
    .WaitFor(stockmodeDb)
    .WaitFor(rabbitMq);

var emailWorker = builder.AddProject<StockMode_EmailWorker>("stockmode-emailworker")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq);

builder.AddNpmApp("stockmode-frontend", "../frontend", "dev", new[] { "--port", "5173" })
    .WithEnvironment("VITE_API_URL", api.GetEndpoint("https"))
    .WaitFor(api)
    .WithNpmPackageInstallation();

builder.Build().Run();
