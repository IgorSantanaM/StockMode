using StockMode.Domain.Core.Data;

namespace StockMode.Domain.Products
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<Product> GetProductBySkuAsync(string sku);
        Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        Task<Variation?> FindVariationByIdAsync(int variationId);
        Task<IEnumerable<Product>> GetActiveProductsWithLowStockAsync(int lowStockThreshold);
    }
}
