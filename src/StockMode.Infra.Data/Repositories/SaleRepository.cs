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
        public SaleRepository(StockModeContext context) : base(context)
        {
            _dbConnection = context.Database.GetDbConnection();
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
                WHERE s.""Status"" = @Status::text
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
                new { Status = status.ToString() },
                splitOn: "Id");

            return saleDictionary.Values;
        }
    }
}
