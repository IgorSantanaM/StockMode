using Dapper;
using MediatR;
using StockMode.Application.Features.Customers.Dtos;
using StockMode.Application.Features.Suppliers.Dtos;
using StockMode.Domain.Common;
using System.Data;
using System.Text;

namespace StockMode.Application.Features.Suppliers.Queries.GetAllSuppliers
{
    public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, PagedResult<SupplierSummaryDto>>
    {
        private readonly IDbConnection _dbConnection;

        public GetAllSuppliersQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<PagedResult<SupplierSummaryDto>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            var whereClauses = new List<string>();

            var countSql = new StringBuilder("SELECT COUNT(*) FROM \"Suppliers\" AS s");
            var selectSql = new StringBuilder(@"
                SELECT
                    s.""Id"",
                    s.""Name"",
                    s.""ContactPerson"",
                    s.""Email"",
                    s.""PhoneNumber""
                FROM ""Suppliers"" AS s
                ");

            if (request.Name is not null)
            {
                whereClauses.Add("s.\"Name\" LIKE @Name");
                parameters.Add("Name", $"%{request.Name}%");
            }

            if (whereClauses.Any())
            {
                var whereSql = " WHERE " + string.Join(" AND ", whereClauses);
                countSql.Append(whereSql);
                selectSql.Append(whereSql);
            }
            selectSql.Append(@"ORDER BY s.""Name""
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

            var offset = (request.Page - 1) * request.PageSize;
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", request.PageSize);

            using (var multi = await _dbConnection.QueryMultipleAsync(countSql.ToString() + ";" + selectSql.ToString(), parameters))
            {
                var totalCount = await multi.ReadSingleAsync<int>();
                var items = (await multi.ReadAsync<SupplierSummaryDto>()).ToList();

                return new PagedResult<SupplierSummaryDto>
                {
                    Items = items,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                };
            }
        }
    }
}
