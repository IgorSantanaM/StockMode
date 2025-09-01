using MediatR;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Domain.Common;

namespace StockMode.Application.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery(int? LowStockThreshold, string? Name, int Page, int PageSize) : IRequest<PagedResult<ProductSummaryDto>>;
