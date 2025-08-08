using StockMode.Domain.Core.Data;

namespace StockMode.Domain.Customers
{
    public interface ICustumerRepository : IRepository<Customer, int>
    {
        Task<Customer?> GetCustomerByName(string name);
    }
}
