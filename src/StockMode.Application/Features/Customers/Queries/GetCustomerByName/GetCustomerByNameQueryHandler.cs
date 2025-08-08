using Dapper;
using MediatR;
using StockMode.Application.Features.Customers.Dtos;
using System.Data;

namespace StockMode.Application.Features.Customers.Queries.GetCustomerByName
{
    public class GetCustomerByNameQueryHandler : IRequestHandler<GetCustomerByNameQuery, IEnumerable<CustomerSummaryDto>>
    {
        private readonly IDbConnection _dbConnection;
        public GetCustomerByNameQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<IEnumerable<CustomerSummaryDto>> Handle(GetCustomerByNameQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT
                    c.""Id"",
                    c.""Name"",
                    c.""Email"",
                    c.""PhoneNumber"",
                    MAX(s.""SaleDate"") AS LastPurchase,
                    COALESCE(SUM(s.""FinalPrice""), 0) AS TotalSpent
                FROM ""Customers"" AS c
                LEFT JOIN ""Sales"" AS s ON c.""Id"" = s.""CustomerId""
                WHERE c.""Name"" ILIKE @SearchTerm
                GROUP BY c.""Id"", c.""Name"", c.""Email"", c.""PhoneNumber""
                ORDER BY c.""Name"";";

            var searchTerm = $"%{request.Name}%"; 
            return await _dbConnection.QueryAsync<CustomerSummaryDto>(sql, new { SearchTerm = searchTerm });
        }
    }
}
