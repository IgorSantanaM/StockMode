using MediatR;
using StockMode.Application.Features.StockMovements.Dtos;
using StockMode.Domain.Common;

namespace StockMode.Application.Features.StockMovements.Queries.GetFullStockMovementReport;

public record GetFullStockMovementReportQuery(DateTime? StartDate, DateTime? EndDate, int? VariationId, int Page, int PageSize) : IRequest<PagedResult<StockMovementDetailsDto>>;

