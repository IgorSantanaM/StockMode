using MediatR;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StockMode.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace StockMode.Infra.Data.Factories
{
    public class StockModeContextFactory : IDesignTimeDbContextFactory<StockModeContext>
    {
        public StockModeContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<StockModeContext>();
            var connectionString = configuration.GetConnectionString("Postgres"); 

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Could not find a connection string named 'Postgres'.");
            }

            optionsBuilder.UseNpgsql(connectionString);

            var mediatorMock = new Mock<IMediator>();

            return new StockModeContext(optionsBuilder.Options, mediatorMock.Object);
        }
    }
}
