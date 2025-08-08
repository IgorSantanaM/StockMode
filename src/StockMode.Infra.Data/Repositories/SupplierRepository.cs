using Dapper;
using Microsoft.EntityFrameworkCore;
using StockMode.Domain.Suppliers;
using StockMode.Infra.Data.Contexts;
using System.Data;

namespace StockMode.Infra.Data.Repositories
{
    public class SupplierRepository : Repository<Supplier, int>, ISupplierRepository
    {
        private IDbConnection _dbConnection;
        private StockModeContext _dbContext;
        public SupplierRepository(StockModeContext context) : base(context)
        {
            _dbConnection = context.Database.GetDbConnection();
            _dbContext = context;
        }

        public Task<Supplier?> GetSupplierByCNPJAsync(string cnpj)
        {
            string sql = @"SELECT * FROM Suppliers WHERE Cnpj = @Cnpj";

            return _dbConnection.QueryFirstOrDefaultAsync<Supplier>(sql, new { Cnpj = cnpj });
        }

        public Task<IEnumerable<Supplier>> GetSupplierByNameAsync(string name)
        {
            string sql = @"SELECT * FROM Suppliers WHERE Name = @Name";

            return _dbConnection.QueryAsync<Supplier>(sql, new { Name = name });
        }
    }
}
