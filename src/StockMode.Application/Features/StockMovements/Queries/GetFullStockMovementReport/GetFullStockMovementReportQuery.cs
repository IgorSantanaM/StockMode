using MediatR;
using StockMode.Application.Features.StockMovements.Dtos;

namespace StockMode.Application.Features.StockMovements.Queries.GetFullStockMovementReport;

public record GetFullStockMovementReportQuery(DateTime StartDate, DateTime EndDate) : IRequest<IEnumerable<StockMovementDetailsDto>>;

