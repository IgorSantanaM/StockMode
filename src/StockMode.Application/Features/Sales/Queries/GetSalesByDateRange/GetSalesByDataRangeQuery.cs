using MediatR;
using StockMode.Application.Features.Sales.Dtos;

namespace StockMode.Application.Features.Sales.Queries.GetSalesByDateRange;

public record GetSalesByDataRangeQuery(DateTime StartDate,
    DateTime EndDate) : IRequest<IReadOnlyCollection<SaleDetailsDto>>;
