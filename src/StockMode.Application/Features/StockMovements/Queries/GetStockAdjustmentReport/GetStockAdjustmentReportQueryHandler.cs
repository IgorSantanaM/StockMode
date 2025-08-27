using Dapper;
using MediatR;
using StockMode.Application.Features.StockMovements.Dtos;
using StockMode.Domain.Common;
using StockMode.Domain.Enums;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace StockMode.Application.Features.StockMovements.Queries.GetStockAdjustmentReport
{
    public class GetStockAdjustmentReportQueryHandler : IRequestHandler<GetStockAdjustmentReportQuery, PagedResult<StockAdjustmentReportDto>>
    {
        private readonly IDbConnection _dbConnection;
        public GetStockAdjustmentReportQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<PagedResult<StockAdjustmentReportDto>> Handle(GetStockAdjustmentReportQuery request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            var whereClauses = new List<string>();

            var countSql = new StringBuilder("SELECT COUNT(*) FROM \"StockMovements\" AS sm");

            var selectSql = new StringBuilder(@"SELECT
                    sm.""MovementDate"",
                    sm.""VariationId"",
                    sm.""Type"" AS MovementType,
                    sm.""Quantity"",
                    sm.""StockAfterMovement"",
                    sm.""Observation""
                FROM ""StockMovements"" AS sm");

            if(request.StartDate.HasValue)
            {
                whereClauses.Add("sm.\"MovementDate\" >= @StartDate");
                parameters.Add("StartDate", request.StartDate.Value);
            }
            if (request.EndDate.HasValue)
            {
                whereClauses.Add("sm.\"MovementDate\" <= @EndDate");
                parameters.Add("EndDate", request.EndDate.Value);
            }


            whereClauses.Add("sm.\"Type\" = @LossAdjustment OR sm.\"Type\" = @PositiveAdjustment");
            parameters.Add("LossAdjustment", StockMovementType.LossAdjustment.ToString());
            parameters.Add("PositiveAdjustment", StockMovementType.PositiveAdjustment.ToString());

            if (whereClauses.Any())
            {
                var whereSql = " WHERE " + string.Join(" AND ", whereClauses);
                countSql.Append(whereSql);
                selectSql.Append(whereSql);
            }

            selectSql.Append(@" ORDER BY sm.""MovementDate"" DESC
                             OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

            var offset = (request.Page - 1) * request.PageSize;
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", request.PageSize);

            using (var multi = await _dbConnection.QueryMultipleAsync(countSql.ToString() + ";" + selectSql.ToString(), parameters))
            {
                var totalCount = await multi.ReadSingleAsync<int>();
                var items = (await multi.ReadAsync<StockAdjustmentReportDto>()).ToList();

                return new PagedResult<StockAdjustmentReportDto>
                {
                    Items = items,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                };
            }
        }
    }
}
