using Dapper;
using MediatR;
using StockMode.Application.Features.Customers.Dtos;
using System.Data;

namespace StockMode.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerSummaryDto>>
    {
        private readonly IDbConnection _dbConnection;

        public GetAllCustomersQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<CustomerSummaryDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT
                    c.""Id"",
                    c.""Name"",
                    c.""Email"",
                    c.""PhoneNumber"",
                    MAX(s.""SaleDate"") AS ""LastPurchase"",
                    COALESCE(SUM(s.""FinalPrice""), 0)::decimal AS ""TotalSpent""
                FROM ""Customers"" AS c
                LEFT JOIN ""Sales"" AS s ON c.""Id"" = s.""CustomerId""
                GROUP BY c.""Id"", c.""Name"", c.""Email"", c.""PhoneNumber""
                ORDER BY c.""Name"";";

            var customers = await _dbConnection.QueryAsync<CustomerSummaryDto>(sql);

            return customers;
        }
    }
}
