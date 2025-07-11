using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Products;
using StockMode.Domain.Sales;
using StockMode.Domain.StockMovements;
using StockMode.Infra.Data.Contexts;
using StockMode.Infra.Data.Repositories;
using StockMode.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Infra.CrossCutting.IoC
{
    public static class ServicesCollection
    {
        public static void AddServices(this IServiceCollection services)
        {

            // Service

            // Application

            // Domain

            // Infra - Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddScoped<IProductRepository, ProductRepository>(); 
            services.AddScoped<ISaleRepository, SaleRepository>(); 
            services.AddScoped<IStockMovementRepository, StockMovementRepository>(); 
        }

        public static void AddContexts(this IServiceCollection services)
        {
            services.AddDbContext<StockModeContext>();
        }

    }
}
