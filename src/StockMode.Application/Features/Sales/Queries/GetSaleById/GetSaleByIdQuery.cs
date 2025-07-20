using MediatR;
using StockMode.Application.Features.Sales.Dtos;

namespace StockMode.Application.Features.Sales.Queries.GetSaleById;

public record GetSaleByIdQuery(int SaleId) : IRequest<SaleDetailsDto>;
