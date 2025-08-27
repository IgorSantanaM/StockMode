using Dapper;
using Microsoft.EntityFrameworkCore;
using StockMode.Domain.Common;
using StockMode.Domain.Customers;
using StockMode.Infra.Data.Contexts;
using System.Data;
using System.Text;

namespace StockMode.Infra.Data.Repositories
{
    public class CustomerRepository : Repository<Customer, int>, ICustomerRepository
    {
        private IDbConnection _dbConnection;
        private StockModeContext _dbContext;
        public CustomerRepository(StockModeContext context) : base(context)
        {
            _dbConnection = context.Database.GetDbConnection();
            _dbContext = context;
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            string sql = @"SELECT * FROM ""Customers"" WHERE ""Id"" = @Id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });
        }
    }
}
