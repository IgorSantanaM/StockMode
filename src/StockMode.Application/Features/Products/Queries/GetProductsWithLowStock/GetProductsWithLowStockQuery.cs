using MediatR;
using StockMode.Application.Features.Products.Dtos;

namespace StockMode.Application.Features.Products.Queries.GetProductsWithLowStock;

public record GetProductsWithLowStockQuery(int LowStockThreshold) : IRequest<IEnumerable<ProductDetailsDto>>;
