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

builder.AddServiceDefaults();
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

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var authority = builder.Configuration["Auth:Authority"] ?? "https://localhost:5001";
        options.Authority = authority;

        var metadataAddress = builder.Configuration["Auth:MetadataAddress"];
        if (!string.IsNullOrEmpty(metadataAddress))
        {
            options.MetadataAddress = metadataAddress;
        }
        
        options.Audience = "stockmodeapi";
        
        options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("Auth:RequireHttpsMetadata", false);

        var validIssuers = builder.Configuration.GetSection("Auth:ValidIssuers").Get<string[]>() 
            ?? new[] { "https://localhost:5001", "http://localhost:5001" };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuers = validIssuers,
            ValidateAudience = true,
            ValidAudience = "stockmodeapi",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };

        // Para desenvolvimento: aceitar certificados SSL não confiáveis
        options.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = ctx =>
            {
                Console.WriteLine($"JWT auth failed: {ctx.Exception.Message}");
                if (ctx.Exception.InnerException != null)
                    Console.WriteLine($"Inner exception: {ctx.Exception.InnerException.Message}");
                return Task.CompletedTask;
            },
            OnChallenge = ctx =>
            {
                Console.WriteLine($"JWT challenge: {ctx.Error} - {ctx.ErrorDescription}");
                return Task.CompletedTask;
            },
            OnTokenValidated = ctx =>
            {
                Console.WriteLine("JWT token validated successfully");
                return Task.CompletedTask;
            },
            OnMessageReceived = ctx =>
            {
                Console.WriteLine($"JWT token received: {ctx.Token?.Substring(0, Math.Min(50, ctx.Token?.Length ?? 0))}...");
                return Task.CompletedTask;
            }
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
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"),
    npgsqlOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null);
    }));

var app = builder.Build();

app.MapDefaultEndpoints();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints<Program>();
app.MapGet("/", () => "StockMode API is running.");

app.Run();
