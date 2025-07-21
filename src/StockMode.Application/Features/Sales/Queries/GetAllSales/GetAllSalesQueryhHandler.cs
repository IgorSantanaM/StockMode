using Dapper;
using MediatR;
using StockMode.Application.Features.Sales.Dtos;
using System.Data;

namespace StockMode.Application.Features.Sales.Queries.GetAllSales
{
    public class GetAllSalesQueryhHandler : IRequestHandler<GetAllSalesQuery, IEnumerable<SaleSummaryDto>>
    {
        private readonly IDbConnection _dbConnection;

        public GetAllSalesQueryhHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<SaleSummaryDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {

            const string sql = @"
                SELECT
                    s.""Id"",
                    s.""SaleDate"",
                    s.""Status"",
                    s.""PaymentMethod"",
                    s.""FinalPrice"",
                    COUNT(si.""Id"") AS ItemCount
                FROM ""Sales"" AS s
                LEFT JOIN ""SaleItem"" AS si ON s.""Id"" = si.""SaleId""
                GROUP BY s.""Id"", s.""SaleDate"", s.""Status"", s.""PaymentMethod"", s.""FinalPrice""
                ORDER BY s.""SaleDate"" DESC;";

            var sales = await _dbConnection.QueryAsync<SaleSummaryDto>(sql);

            return sales;
        }
    }
}
