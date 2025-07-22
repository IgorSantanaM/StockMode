using MediatR;
using StockMode.Application.Features.StockMovements.Dtos;
using StockMode.Domain.StockMovements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.StockMovements.Queries.GetFullStockMovementReport
{
    public class GetFullStockMovementReportQueryHandler : IRequestHandler<GetFullStockMovementReportQuery, IEnumerable<StockMovementDetailsDto>>
    {
        private readonly IStockMovementRepository _stockMovementRepository;

        public GetFullStockMovementReportQueryHandler(IStockMovementRepository stockMovementRepository)
        {
            _stockMovementRepository = stockMovementRepository;
        }

        public async Task<IEnumerable<StockMovementDetailsDto>> Handle(GetFullStockMovementReportQuery request, CancellationToken cancellationToken)
        {
            var movements = await _stockMovementRepository.GetMovementsByDateRangeAsync(request.StartDate, request.EndDate);

            var report = movements.Select(m => new StockMovementDetailsDto(
                m.VariationId,
                m.SaleId,
                m.Type,
                m.Quantity,
                m.StockAfterMovement,
                m.Note,
                m.Observation,
                m.MovementDate));

            return report;
        }
    }
}
