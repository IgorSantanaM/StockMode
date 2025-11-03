using MediatR;

namespace StockMode.Application.Features.Sales.Queries.GetPdfSaleById;

public record GetPdfSaleByIdQuery(int Id) : IRequest<byte[]>;
