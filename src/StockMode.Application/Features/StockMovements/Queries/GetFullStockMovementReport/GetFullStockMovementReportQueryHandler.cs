using Dapper;
using MediatR;
using StockMode.Application.Features.Customers.Dtos;
using StockMode.Application.Features.StockMovements.Dtos;
using StockMode.Domain.Common;
using StockMode.Domain.StockMovements;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.StockMovements.Queries.GetFullStockMovementReport
{
    public class GetFullStockMovementReportQueryHandler : IRequestHandler<GetFullStockMovementReportQuery, PagedResult<StockMovementDetailsDto>>
    {
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IDbConnection _dbConnection;

        public GetFullStockMovementReportQueryHandler(IStockMovementRepository stockMovementRepository, IDbConnection dbConnection)
        {
            _stockMovementRepository = stockMovementRepository;
            _dbConnection = dbConnection;
        }

        public async Task<PagedResult<StockMovementDetailsDto>> Handle(GetFullStockMovementReportQuery request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            var whereClauses = new List<string>();

            var countSql = new StringBuilder("SELECT COUNT(*) FROM \"StockMovements\"");
            var selectSql = new StringBuilder(@"SELECT * FROM ""StockMovements""");

            if(request.StartDate.HasValue)
            {
                whereClauses.Add("\"MovementDate\" >= @StartDate");
                parameters.Add("StartDate", request.StartDate);
            }

            if(request.EndDate.HasValue)
            {
                whereClauses.Add("\"MovementDate\" <= @EndDate");
                parameters.Add("EndDate", request.EndDate);
            }

            if(request.VariationId.HasValue)
            {
                whereClauses.Add("\"VariationId\" = @VariationId");
                parameters.Add("VariationId", request.VariationId);
            }

            if (whereClauses.Any())
            {
                var whereSql = " WHERE " + string.Join(" AND ", whereClauses);
                countSql.Append(whereSql);
                selectSql.Append(whereSql);
            }

            selectSql.Append(@"ORDER BY ""MovementDate"" DESC
                            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

            var offset = (request.Page - 1) * request.PageSize;
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", request.PageSize);

            using (var multi = await _dbConnection.QueryMultipleAsync(countSql.ToString() + ";" + selectSql.ToString(), parameters))
            {
                var totalCount = await multi.ReadSingleAsync<int>();
                var items = (await multi.ReadAsync<StockMovementDetailsDto>()).ToList();

                return new PagedResult<StockMovementDetailsDto>
                {
                    Items = items,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                };
            }
        }
    }
}
