using StockMode.Domain.Core.Data;

namespace StockMode.Domain.Customers
{
    public interface ICustumerRepository : IRepository<Customer, int>
    {
        Task<Customer?> GetCustomerById(int id);
        Task<Customer?> GetCustomerByName(string name);
    }
}
