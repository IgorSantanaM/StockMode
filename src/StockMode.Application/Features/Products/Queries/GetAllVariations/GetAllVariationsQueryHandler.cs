using Dapper;
using MediatR;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Products.Queries.GetAllVariations
{
    public class GetAllVariationsQueryHandler : IRequestHandler<GetAllVariationsQuery, PagedResult<VariationDetailDto>>
    {
        private readonly IDbConnection _dbConnection;
        public GetAllVariationsQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<PagedResult<VariationDetailDto>> Handle(GetAllVariationsQuery request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            var whereClauses = new List<string>();

            var countSql = new StringBuilder("SELECT COUNT(*) FROM \"Variations\" v");
            var selectSql = new StringBuilder(@"
                                            SELECT
                                                v.""Id"",
                                                v.""Name"",
                                                v.""Sku"",
                                                v.""CostPrice"",
                                                v.""SalePrice"",
                                                v.""StockQuantity""
                                            FROM ""Variations"" AS v");

            if (!string.IsNullOrWhiteSpace(request.name))
            {
                whereClauses.Add("v.\"Name\" ILIKE @Name");
                parameters.Add("Name", $"%{request.name}%");
            }

            if (!string.IsNullOrWhiteSpace(request.sku))
            {
                whereClauses.Add("v.\"Sku\" ILIKE @Sku");
                parameters.Add("Sku", $"%{request.sku}%");
            }

            if (whereClauses.Any())
            {
                var whereSql = " WHERE " + string.Join(" AND ", whereClauses);
                countSql.Append(whereSql);
                selectSql.Append(whereSql);
            }

            selectSql.Append(" ORDER BY v.\"Id\"");

            selectSql.Append(" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var offset = (request.Page - 1) * request.PageSize;
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", request.PageSize);

            var finalSql = $"{countSql}; {selectSql};";

            using (var multi = await _dbConnection.QueryMultipleAsync(finalSql, parameters))
            {
                var totalCount = await multi.ReadSingleAsync<int>();
                var items = (await multi.ReadAsync<VariationDetailDto>()).ToList();

                return new PagedResult<VariationDetailDto>
                {
                    Items = items,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                };
            }
        }
    }
}
