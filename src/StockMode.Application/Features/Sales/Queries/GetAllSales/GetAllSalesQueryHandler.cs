using Dapper;
using MediatR;
using StockMode.Application.Features.Customers.Dtos;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Common;
using System.Data;
using System.Text;

namespace StockMode.Application.Features.Sales.Queries.GetAllSales
{
    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, PagedResult<SaleSummaryDto>>
    {
        private readonly IDbConnection _dbConnection;

        public GetAllSalesQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<PagedResult<SaleSummaryDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            try
            {
               var parameters = new DynamicParameters();
                var whereClauses = new List<string>();

                var countSql = new StringBuilder("SELECT COUNT(*) FROM \"Sales\" AS s");

                var selectSql = new StringBuilder(@"
                SELECT
                    s.""Id"",
                    s.""SaleDate"",
                    s.""Status"",
                    s.""PaymentMethod"",
                    s.""FinalPrice"",
                    COUNT(si.""Id"") AS ItemCount
                FROM ""Sales"" AS s
                LEFT JOIN ""SaleItems"" AS si ON s.""Id"" = si.""SaleId""");

                if (request.StartDate.HasValue)
                {
                    whereClauses.Add("s.\"SaleDate\" >= @StartDate");
                    parameters.Add("StartDate", request.StartDate.Value);
                }

                if (request.EndDate.HasValue)
                {
                    whereClauses.Add("s.\"SaleDate\" <= @EndDate");
                    parameters.Add("EndDate", request.EndDate.Value);
                }

                if (request.Status.HasValue)
                {
                    whereClauses.Add("s.\"Status\" = @Status ");
                    parameters.Add("Status", request.Status.Value.ToString());
                }

                if (whereClauses.Any())
                {
                    var whereSql = " WHERE " + string.Join(" AND ", whereClauses);
                    countSql.Append(whereSql);
                    selectSql.Append(whereSql);
                }

                selectSql.Append(@"GROUP BY s.""Id"", s.""SaleDate"", s.""Status"", s.""PaymentMethod"", s.""FinalPrice""
                ORDER BY s.""SaleDate"" DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

                var offset = (request.Page - 1) * request.PageSize;
                parameters.Add("Offset", offset);
                parameters.Add("PageSize", request.PageSize);

                using (var multi = await _dbConnection.QueryMultipleAsync(countSql.ToString() + ";" + selectSql.ToString(), parameters))
                {
                    var totalCount = await multi.ReadSingleAsync<int>();
                    var items = (await multi.ReadAsync<SaleSummaryDto>()).ToList();

                    return new PagedResult<SaleSummaryDto>
                    {
                        Items = items,
                        TotalCount = totalCount,
                        TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                    };
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
