using MediatR;
using StockMode.Application.Features.Tags.Dtos;
using StockMode.Domain.Common;

namespace StockMode.Application.Features.Tags.Queries.GetAllTags;

public record GetAllTagsQuery(int Page, int PageSize, string? Name) : IRequest<PagedResult<TagsSummaryDto>>;
