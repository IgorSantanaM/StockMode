using MediatR;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Enums;

namespace StockMode.Application.Features.Sales.Queries.GetSalesByStatus;

public record GetSalesByStatusQuery(SaleStatus Status) : IRequest<IReadOnlyCollection<SaleDetailsDto>>;
