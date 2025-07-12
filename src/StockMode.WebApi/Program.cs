using MediatR;
using Microsoft.EntityFrameworkCore;
using StockMode.Application.Features.Products.Commands.CreateProduct;
using StockMode.Infra.CrossCutting.IoC;
using StockMode.Infra.Data.Contexts;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddServices();

services.AddDbContext<StockModeContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "StockMode API V1");
        options.DocumentTitle = "StockMode API Documentation";
        options.DefaultModelExpandDepth(-1);
    });

    app.UseDeveloperExceptionPage();
}

app.MapPost("/api/products", async (CreateProductCommand product, IMediator mediator) =>
{

    var id = await mediator.Send(product);

    return Results.Created();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
