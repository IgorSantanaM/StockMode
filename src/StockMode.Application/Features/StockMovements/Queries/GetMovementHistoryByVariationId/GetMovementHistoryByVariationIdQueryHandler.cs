using MediatR;
using StockMode.Application.Features.StockMovements.Dtos;
using StockMode.Domain.StockMovements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.StockMovements.Queries.GetMovementHistoryByVariationId
{
    public class GetMovementHistoryByVariationIdQueryHandler : IRequestHandler<GetMovementHistoryByVariationIdQuery, IEnumerable<StockMovementDetailsDto>>
    {
        private readonly IStockMovementRepository _stockMovementRepository;
        public GetMovementHistoryByVariationIdQueryHandler(IStockMovementRepository stockMovementRepository)
        {
            _stockMovementRepository = stockMovementRepository ?? throw new ArgumentNullException(nameof(stockMovementRepository));
        }
        public async Task<IEnumerable<StockMovementDetailsDto>> Handle(GetMovementHistoryByVariationIdQuery request, CancellationToken cancellationToken)
        {
            var movements = await _stockMovementRepository.GetMovementsByVariationIdAsync(request.VariationId);
            var report =  movements.Select(m => new StockMovementDetailsDto(
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
