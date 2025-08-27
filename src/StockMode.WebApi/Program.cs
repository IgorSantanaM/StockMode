using EasyNetQ;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockMode.Infra.CrossCutting.IoC;
using StockMode.Infra.Data.Contexts;
using StockMode.WebApi.Endpoints.Internal;
using StockMode.WebApi.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

services.AddCors(opt =>
{
    opt.AddPolicy(name: MyAllowSpecificOrigins,
                 policy =>
                 {
                     policy.AllowAnyOrigin();
                     policy.AllowAnyMethod();
                     policy.AllowAnyHeader();
                 });
});

services.AddControllers();
services.AddMailServices(builder.Configuration);

services.ConfigureOpenTelemetry(builder.Configuration);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddServices();

builder.Services.AddDbContext<StockModeContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    npgsqlOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null);
    }));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    try
    {
        var context = service.GetRequiredService<StockModeContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = service.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrações do banco de dados.");
    }
}

app.UseCors(MyAllowSpecificOrigins);

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "StockMode API V1");
        options.DocumentTitle = "StockMode API Documentation";
        options.DefaultModelExpandDepth(-1);
    });
}

//app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints<Program>();

app.Run();
