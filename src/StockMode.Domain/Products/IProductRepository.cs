using StockMode.Domain.Core.Data;

namespace StockMode.Domain.Products
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<Product> GetProductBySkuAsync(string sku);
        Task<Variation?> FindVariationByIdAsync(int variationId);
        Task<IEnumerable<Product>> GetActiveProductsWithLowStockAsync(int lowStockThreshold);
    }
}
