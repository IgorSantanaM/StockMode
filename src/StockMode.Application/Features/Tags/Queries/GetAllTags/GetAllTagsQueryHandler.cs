using Dapper;
using MediatR;
using StockMode.Application.Features.Tags.Dtos;
using StockMode.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Tags.Queries.GetAllTags
{
    public class GetAllTagsQueryHandler(IDbConnection dbConnection) : IRequestHandler<GetAllTagsQuery, PagedResult<TagsSummaryDto>>
    {
        
        public async Task<PagedResult<TagsSummaryDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            var whereClauses = new List<string>();

            var countSql = new StringBuilder(@"SELECT COUNT(*) FROM ""Tags""");
            var selectSql = new StringBuilder(@"SELECT ""Name"", ""Color"" FROM ""Tags""");

            if(request.Name is not null)
            {
                whereClauses.Add(@"""Name"" LIKE @Name");
                parameters.Add(@"Name", $"%{request.Name}%");
            }

            if(whereClauses.Any())
            {
                var whereClause = " WHERE " + string.Join(" AND ", whereClauses);
                countSql.Append(whereClause);
                selectSql.Append(whereClause);
            }
            
            selectSql.Append(@" ORDER BY ""Name"" ASC
                             OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
                            ");
            var offSet = (request.Page - 1) * request.PageSize;
            parameters.Add("@Offset", offSet);
            parameters.Add("@PageSize", request.PageSize);

            using(var multi = await dbConnection.QueryMultipleAsync(countSql.ToString() + ";" + selectSql.ToString(), parameters))
            {
                var totalCount = await multi.ReadSingleAsync<int>();
                var items = (await multi.ReadAsync<TagsSummaryDto>()).ToList();
                return new PagedResult<TagsSummaryDto>
                {
                    Items = items,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                };
            }
        }
    }
}
