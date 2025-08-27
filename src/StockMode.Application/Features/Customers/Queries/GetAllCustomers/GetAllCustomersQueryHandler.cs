using Dapper;
using MediatR;
using StockMode.Application.Features.Customers.Dtos;
using StockMode.Domain.Common;
using StockMode.Domain.Customers;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace StockMode.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, PagedResult<CustomerSummaryDto>>
    {
        private readonly IDbConnection _dbConnection;
        public GetAllCustomersQueryHandler(IDbConnection dbConnection, ICustomerRepository customerRepository)
        {
            _dbConnection = dbConnection;
        }

        public async Task<PagedResult<CustomerSummaryDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            var whereClauses = new List<string>();

            var countSql = new StringBuilder("SELECT COUNT(*) FROM \"Customers\"");
            var selectSql = new StringBuilder(@"
                SELECT
                    c.""Id"",
                    c.""Name"",
                    c.""Email"",
                    c.""PhoneNumber"",
                    MAX(s.""SaleDate"") AS ""LastPurchase"",
                    COALESCE(SUM(s.""FinalPrice""), 0)::decimal AS ""TotalSpent""
                FROM ""Customers"" AS c
                LEFT JOIN ""Sales"" AS s ON c.""Id"" = s.""CustomerId""
                ");

            if (request.Name is not null)
            {
                whereClauses.Add("\"Name\" LIKE @Name");
                parameters.Add("Name", $"%{request.Name}%");
            }

            if (whereClauses.Any())
            {
                var whereSql = " WHERE " + string.Join(" AND ", whereClauses);
                countSql.Append(whereSql);
                selectSql.Append(whereSql);
            }

            selectSql.Append(@"
                GROUP BY c.""Id"", c.""Name"", c.""Email"", c.""PhoneNumber""
                ORDER BY c.""Name""
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
            ");

            var offset = (request.Page - 1) * request.PageSize;
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", request.PageSize);


            using (var multi = await _dbConnection.QueryMultipleAsync(countSql.ToString() + ";" + selectSql.ToString(), parameters))
            {
                var totalCount = await multi.ReadSingleAsync<int>();
                var items = (await multi.ReadAsync<CustomerSummaryDto>()).ToList();

                return new PagedResult<CustomerSummaryDto>
                {
                    Items = items,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                };
            }
        }
    }
}
