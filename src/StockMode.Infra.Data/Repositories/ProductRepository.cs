using Dapper;
using Microsoft.EntityFrameworkCore;
using StockMode.Domain.Products;
using StockMode.Infra.Data.Contexts;
using System.Data;

namespace StockMode.Infra.Data.Repositories
{
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        private readonly IDbConnection _dbConnection;
        public ProductRepository(StockModeContext context) : base(context)
        {
            _dbConnection = context.Database.GetDbConnection();
        }

        public async Task<Variation?> FindVariationByIdAsync(int variationId)
        {
            const string sql = @"
                SELECT * FROM ""Variations"" AS v
                INNER JOIN ""Products"" AS p ON v.""ProductId"" = p.""Id""
                WHERE v.""Id"" = @VariationId;";

            var variation = await _dbConnection.QueryAsync<Variation, Product, Variation>(
                sql,
                (variation, product) =>
                {
                    variation.Product = product;
                    return variation;
                },
                new { VariationId = variationId },
                splitOn: "Id"); 

            return variation.FirstOrDefault();
        }
        public async Task<Product> GetProductBySkuAsync(string sku)
        {
            const string sql = @"
                SELECT p.*, v.* FROM ""Products"" AS p
                INNER JOIN ""Variations"" AS v ON p.""Id"" = v.""ProductId""
                WHERE v.""Sku"" = @Sku;";

            var productDictionary = new Dictionary<int, Product>();

            var products = await _dbConnection.QueryAsync<Product, Variation, Product>(
                sql,
                (product, variation) =>
                {
                    if (!productDictionary.TryGetValue(product.Id, out var productEntry))
                    {
                        productEntry = product;
                        productDictionary.Add(productEntry.Id, productEntry);
                    }

                    productEntry.Variations.Add(variation);
                    return productEntry;
                },
                new { Sku = sku },
                splitOn: "Id");

            return products.FirstOrDefault()!;
        }

        public async Task<IEnumerable<Product>> GetActiveProductsWithLowStockAsync(int lowStockThreshold)
        {
            const string sql = @"
                SELECT p.*, v.*
                FROM ""Products"" p
                INNER JOIN ""Variations"" v ON p.""Id"" = v.""ProductId""
                WHERE p.""IsActive"" = TRUE AND p.""Id"" IN (
                    SELECT ""ProductId""
                    FROM ""Variations""
                    WHERE ""StockQuantity"" <= @LowStockThreshold
                );";

            var productDictionary = new Dictionary<int, Product>();

            await _dbConnection.QueryAsync<Product, Variation, Product>(
                sql,
                (product, variation) =>
                {
                    if (!productDictionary.TryGetValue(product.Id, out var productEntry))
                    {
                        productEntry = product;
                        productDictionary.Add(productEntry.Id, productEntry);
                    }

                    productEntry.Variations.Add(variation);
                    return productEntry;
                },
                new { LowStockThreshold = lowStockThreshold },
                splitOn: "Id");

            return productDictionary.Values;
        }
    }
}
