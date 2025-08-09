using StockMode.Domain.Core.Data;

namespace StockMode.Domain.Suppliers
{
    public interface ISupplierRepository : IRepository<Supplier, int>
    {
        Task<Supplier> GetSupplierById(int id);
    }
}
