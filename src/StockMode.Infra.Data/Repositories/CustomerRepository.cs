using Dapper;
using Microsoft.EntityFrameworkCore;
using StockMode.Domain.Customers;
using StockMode.Infra.Data.Contexts;
using System.Data;

namespace StockMode.Infra.Data.Repositories
{
    public class CustomerRepository : Repository<Customer, int>, ICustumerRepository
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

        public async Task<Customer?> GetCustomerByName(string name)
        {
            string sql = @"SELECT * FROM ""Customers"" WHERE ""Name"" = @Name";

            return await _dbConnection.QueryFirstOrDefaultAsync<Customer>(sql, new { Name = name });
        }
    }
}
