using MediatR;
using System.Data;
using Dapper;
using StockMode.Application.Features.Products.Dtos;
using System.Text;
using StockMode.Application.Features.Customers.Dtos;
using StockMode.Domain.Common;

namespace StockMode.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResult<ProductSummaryDto>>
    {
        private readonly IDbConnection _dbConnection;

        public GetAllProductsQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<PagedResult<ProductSummaryDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            var whereClauses = new List<string>();

            var countSql = new StringBuilder("SELECT COUNT(*) FROM \"Products\"");
            StringBuilder selectSql = new StringBuilder(@"
                SELECT
                    p.""Id"",
                    p.""Name"",
                    p.""Description"",
                    p.""IsActive"",
                    COUNT(v.""Id"") AS VariationCount,
                    MIN(v.""SalePrice"") AS MinSalePrice,
                    MAX(v.""SalePrice"") AS MaxSalePrice,
                    AVG(v.""CostPrice"") AS AverageCostPrice,
                    COALESCE(SUM(v.""StockQuantity""), 0) AS TotalStockQuantity
                FROM ""Products"" AS p
                LEFT JOIN ""Variations"" AS v ON p.""Id"" = v.""ProductId""
                ");

            if(request.Sku is not null)
            {
                whereClauses.Add("v.\"Sku\" LIKE @Sku");
                parameters.Add("Sku", $"%{request.Sku}%");
            }
            if (request.LowStockThreshold.HasValue)
            {
                whereClauses.Add("v.\"StockQuantity\" <= @LowStockThreshold");
                parameters.Add("LowStockThreshold", request.LowStockThreshold);
            }

            if(whereClauses.Any())
            {
                var whereSql = " WHERE " + string.Join(" AND ", whereClauses);
                countSql.Append(whereSql);
                selectSql.Append(whereSql);
            }

            selectSql.Append(@"GROUP BY p.""Id"", p.""Name"", p.""Description"", p.""IsActive""
                ORDER BY p.""Name""
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

            var offset = (request.Page - 1) * request.PageSize;
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", request.PageSize);

            using (var multi = await _dbConnection.QueryMultipleAsync(countSql.ToString() + ";" + selectSql.ToString(), parameters))
            {
                var totalCount = await multi.ReadSingleAsync<int>();
                var items = (await multi.ReadAsync<ProductSummaryDto>()).ToList();

                return new PagedResult<ProductSummaryDto>
                {
                    Items = items,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                };
            }
        }
    }
}
