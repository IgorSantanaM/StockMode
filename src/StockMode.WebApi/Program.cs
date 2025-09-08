using EasyNetQ;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockMode.Infra.CrossCutting.IoC;
using StockMode.Infra.Data.Contexts;
using StockMode.WebApi.Diagnostics.Extensions;
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

// Configure JWT Authentication
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://stockmode.idp";
        options.RequireHttpsMetadata = false; // For development
        options.Audience = "stockmodeapi";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    });

services.AddAuthorization();

services.AddControllers();
services.AddMailServices(builder.Configuration);

builder.AddOpenTelemetry();
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
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migra��es do banco de dados.");
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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints<Program>();
app.MapGet("/", () => "StockMode API is running.");

app.Run();
