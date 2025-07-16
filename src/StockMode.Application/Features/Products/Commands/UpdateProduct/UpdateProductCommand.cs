using MediatR;
using StockMode.Application.Features.Products.Dtos;

namespace StockMode.Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(
        int ProductId,
        string Name,
        string? Description,
        IReadOnlyCollection<VariationCommandDto> Variations) : IRequest<ProductDetailsDto>;
}
