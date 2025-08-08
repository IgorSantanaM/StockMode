using Dapper;
using MediatR;
using StockMode.Application.Features.Customers.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Customers.Queries.GetCustomerByName
{
    public class GetCustomerByNameQueryHandler : IRequestHandler<GetCustomerByNameQuery, CustomerDetailsDto>
    {
        private readonly IDbConnection _dbConnection;
        public GetCustomerByNameQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<CustomerDetailsDto> Handle(GetCustomerByNameQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT
                    c.""Id"",
                    c.""Name"",
                    c.""Email"",
                    c.""PhoneNumber"",
                    c.""Street"", 
                    c.""City"", 
                    c.""State"", 
                    c.""ZipCode"",
                    MAX(s.""SaleDate"") AS LastPurchase,
                    COALESCE(SUM(s.""FinalPrice""), 0) AS TotalSpent
                FROM ""Customers"" AS c
                LEFT JOIN ""Sales"" AS s ON c.""Id"" = s.""CustomerId""
                WHERE c.""Name"" = @Name
                GROUP BY c.""Id"", c.""Name"", c.""Email"", c.""PhoneNumber"", c.""Street"", c.""City"", c.""State"", c.""ZipCode"";";

            var customerDetailsDto = await _dbConnection.QueryFirstOrDefaultAsync<CustomerDetailsDto>(sql, new { request.Name });

            return customerDetailsDto!;
        }
    }
}
