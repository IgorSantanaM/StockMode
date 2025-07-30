using EasyNetQ;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockMode.Application.Features.Products.Commands.CreateProduct;
using StockMode.Infra.CrossCutting.IoC;
using StockMode.Infra.Data.Contexts;
using StockMode.WebApi.Endpoints.Internal;
using StockMode.WebApi.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
services.AddMailServices(builder.Configuration);
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

app.UseAuthorization();

app.UseEndpoints<Program>();

app.Run();
