using Dapper;
using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Application.Features.Customers.Dtos;
using StockMode.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDetailsDto>
    {
        private readonly IDbConnection _dbConnection;
        public GetCustomerByIdQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<CustomerDetailsDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
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
                WHERE c.""Id"" = @CustomerId
                GROUP BY c.""Id"", c.""Name"", c.""Email"", c.""PhoneNumber"", c.""Street"", c.""City"", c.""State"", c.""ZipCode"";";

            var customerDetailsDto = await _dbConnection.QueryFirstOrDefaultAsync<CustomerDetailsDto>(sql, new { request.CustomerId });

            if (customerDetailsDto is null)
                throw new NotFoundException(nameof(Customer), request.CustomerId);

            return customerDetailsDto!;
        }
    }
}
