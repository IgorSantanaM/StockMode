using StockMode.Domain.Common;
using StockMode.Domain.Core.Data;

namespace StockMode.Domain.Customers
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
        Task<Customer?> GetCustomerById(int id);
    }
}
