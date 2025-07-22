using MediatR;
using StockMode.Application.Features.StockMovements.Dtos;

namespace StockMode.Application.Features.StockMovements.Queries.GetStockAdjustmentReport;

public record GetStockAdjustmentReportQuery(DateTime StartDate, DateTime EndDate) : IRequest<IEnumerable<StockAdjustmentReportDto>>;
