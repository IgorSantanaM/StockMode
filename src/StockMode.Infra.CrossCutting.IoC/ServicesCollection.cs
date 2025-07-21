using Microsoft.Extensions.DependencyInjection;
using StockMode.Application.Features.Products.Commands.CreateProduct;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Products;
using StockMode.Domain.Sales;
using StockMode.Domain.StockMovements;
using StockMode.Infra.Data.Contexts;
using StockMode.Infra.Data.Repositories;
using StockMode.Infra.Data.UoW;
using StockMode.Application.Features.Products.Validators;
using FluentValidation;
using StockMode.Application.Features.Products.Queries.GetProductById;
using StockMode.Application.Features.Products.Queries.GetAllProducts;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace StockMode.Infra.CrossCutting.IoC
{
    public static class ServicesCollection
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Application
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly)
            );

            services.AddScoped<IDbConnection>(sp =>
                sp.GetRequiredService<StockModeContext>().Database.GetDbConnection());

            services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);

            // Domain

            // Infra - Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IStockMovementRepository, StockMovementRepository>();
        }
    }
}
