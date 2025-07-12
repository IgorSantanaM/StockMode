using MediatR;
using StockMode.Application.DTOs;

namespace StockMode.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name, string? Description, IReadOnlyCollection<VariationCommandDto> Variations) : IRequest<int>;
}
