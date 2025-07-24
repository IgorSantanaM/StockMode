using Dapper;
using MediatR;
using StockMode.Application.Features.StockMovements.Dtos;
using StockMode.Domain.Enums;
using System.Data;
using System.Linq.Expressions;

namespace StockMode.Application.Features.StockMovements.Queries.GetStockAdjustmentReport
{
    public class GetStockAdjustmentReportQueryHandler : IRequestHandler<GetStockAdjustmentReportQuery, IEnumerable<StockAdjustmentReportDto>>
    {
        private readonly IDbConnection _dbConnection;
        public GetStockAdjustmentReportQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<StockAdjustmentReportDto>> Handle(GetStockAdjustmentReportQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT
                    sm.""MovementDate"",
                    sm.""VariationId"",
                    sm.""Type"" AS MovementType,
                    sm.""Quantity"",
                    sm.""StockAfterMovement"",
                    sm.""Observation""
                FROM ""StockMovements"" AS sm
                WHERE 
                    (sm.""Type"" = @LossAdjustment OR sm.""Type"" = @PositiveAdjustment)
                    AND sm.""MovementDate"" >= @StartDate AND sm.""MovementDate"" <= @EndDate
                ORDER BY sm.""MovementDate"" DESC;";

            var parameters = new
            {
                request.StartDate,
                request.EndDate,
                LossAdjustment = (int)StockMovementType.LossAdjustment,
                PositiveAdjustment = (int)StockMovementType.PositiveAdjustment
            };
            var report = await _dbConnection.QueryAsync<StockAdjustmentReportDto>(sql, parameters);

            return report;
        }
    }
}
