using MediatR;
using StockMode.Application.Features.Tags.Dtos;

namespace StockMode.Application.Features.Tags.Queries.GetTagById;
public record GetTagByIdQuery(int id) : IRequest<TagDetailsDto>;