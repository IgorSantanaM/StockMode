using MediatR;
using StockMode.Application.Features.Products.Dtos;

namespace StockMode.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(string Name, string? Description, IReadOnlyCollection<VariationCommandDto> Variations) : IRequest<int>;
