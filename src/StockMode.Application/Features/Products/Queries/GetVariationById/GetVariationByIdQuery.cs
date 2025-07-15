using MediatR;
using StockMode.Application.Features.Products.Dtos;

namespace StockMode.Application.Features.Products.Queries.GetVariationById;

public record GetVariationByIdQuery(int VariationId) : IRequest<VariationDetailDto>;
