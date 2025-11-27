using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockMode.Infra.Data.Contexts;
using StockMode.WebApi;
using Testcontainers.PostgreSql;

namespace StockMode.Presentation.WebApi.Tests.Integration
{
    public class StockModeApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer =
             new PostgreSqlBuilder()
                .WithDatabase("stockmode_test_db")
                .WithUsername("postgres")
                .WithPassword("postgrespw")
                .WithImage("postgres:15-alpine")
                .WithPortBinding(0, 5432)
                .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            });

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<StockModeContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<StockModeContext>(options =>
                {
                    options.UseNpgsql(_dbContainer.GetConnectionString());
                });

                services.AddHttpClient();
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StockModeContext>();
            await context.Database.MigrateAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _dbContainer.StopAsync();
            await _dbContainer.DisposeAsync();
        }
    }
}
