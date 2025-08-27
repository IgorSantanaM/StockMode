using MediatR;
using StockMode.Application.Features.StockMovements.Dtos;
using StockMode.Domain.Common;

namespace StockMode.Application.Features.StockMovements.Queries.GetStockAdjustmentReport;

public record GetStockAdjustmentReportQuery(DateTime? StartDate, DateTime? EndDate, int Page, int PageSize) : IRequest<PagedResult<StockAdjustmentReportDto>>;
