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

        public async Task<Supplier?> GetSupplierById(int id)
        {
            const string sql = @"SELECT * FROM ""Suppliers"" WHERE ""Id"" = @SupplierId;";

            return await _dbConnection.QueryFirstOrDefaultAsync<Supplier>(sql, new { SupplierId = id });
        }
    }
}
