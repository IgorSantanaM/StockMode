using MediatR;
using StockMode.Application.Features.Products.Dtos;

namespace StockMode.Application.Features.Products.Queries.GetProductBySku;

public record GetProductBySkuQuery(string Sku) : IRequest<ProductDetailsDto>;
