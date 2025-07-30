using Dapper;
using Microsoft.EntityFrameworkCore;
using StockMode.Domain.Enums;
using StockMode.Domain.Sales;
using StockMode.Infra.Data.Contexts;
using System.Data;

namespace StockMode.Infra.Data.Repositories
{
    public class SaleRepository : Repository<Sale, int>, ISaleRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly StockModeContext _dbContext;
        public SaleRepository(StockModeContext context, StockModeContext dbContext) : base(context)
        {   
            _dbConnection = context.Database.GetDbConnection();
            _dbContext = dbContext;
        }

        public async Task<Sale?> GetSaleByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Sales
                 .Include(s => s.Items)
                 .FirstOrDefaultAsync(sale => sale.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT s.*, si.* FROM ""Sales"" AS s
                INNER JOIN ""SaleItems"" AS si ON s.""Id"" = si.""SaleId""
                WHERE s.""SaleDate"" >= @StartDate AND s.""SaleDate"" <= @EndDate
                ORDER BY s.""SaleDate"" DESC;";

            var saleDictionary = new Dictionary<int, Sale>();

            var sales = await _dbConnection.QueryAsync<Sale, SaleItem, Sale>(
                sql,
                (sale, saleItem) =>
                {
                    if (!saleDictionary.TryGetValue(sale.Id, out var saleEntry))
                    {
                        saleEntry = sale;
                        saleDictionary.Add(saleEntry.Id, saleEntry);
                    }

                    saleEntry.Items.Add(saleItem);
                    return saleEntry;
                },
                new { StartDate = startDate, EndDate = endDate },
                splitOn: "Id");

            return saleDictionary.Values;
        }

        public async Task<IEnumerable<Sale>> GetSalesByStatusAsync(SaleStatus status)
        {
            const string sql = @"
                SELECT s.*, si.*
                FROM ""Sales"" AS s
                INNER JOIN ""SaleItems"" AS si ON s.""Id"" = si.""SaleId""
                WHERE s.""Status"" = @Status
                ORDER BY s.""SaleDate"" DESC;";

            var saleDictionary = new Dictionary<int, Sale>();

            var sales = await _dbConnection.QueryAsync<Sale, SaleItem, Sale>(
                sql,
                (sale, saleItem) =>
                {
                    if (!saleDictionary.TryGetValue(sale.Id, out var saleEntry))
                    {
                        saleEntry = sale;
                        saleDictionary.Add(saleEntry.Id, saleEntry);
                    }

                    saleEntry.Items.Add(saleItem);
                    return saleEntry;
                },
                new { Status = status },
                splitOn: "Id");

            return saleDictionary.Values;
        }
    }
}
