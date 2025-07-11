using Dapper;
using Microsoft.EntityFrameworkCore;
using StockMode.Domain.StockMovements;
using StockMode.Infra.Data.Contexts;
using System.Data;
using System.Data.Common;

namespace StockMode.Infra.Data.Repositories
{
    public class StockMovementRepository : Repository<StockMovement, int>, IStockMovementRepository
    {
        private readonly IDbConnection _dbConnection;
        public StockMovementRepository(StockModeContext context) : base(context)
        {
            _dbConnection = context.Database.GetDbConnection();
        }

        public async Task<IEnumerable<StockMovement>> GetMovementsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT * FROM ""StockMovements"" 
                WHERE ""MovementDate"" >= @StartDate AND ""MovementDate"" <= @EndDate
                ORDER BY ""MovementDate"" DESC;";

            return await _dbConnection.QueryAsync<StockMovement>(sql, new {StartDate = startDate, EndDate = endDate});
        }

        public async Task<IEnumerable<StockMovement>> GetMovementsByVariationIdAsync(int variationId)
        {
            const string sql = @"
                SELECT * FROM ""StockMovements"" 
                WHERE ""VariationId"" = @VariationId
                ORDER BY ""MovementDate"" DESC;";

            return await _dbConnection.QueryAsync<StockMovement>(sql, new { VariationId = variationId });
        }
    }
}
