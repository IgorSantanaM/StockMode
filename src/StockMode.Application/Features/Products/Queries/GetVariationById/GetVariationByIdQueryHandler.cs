using MediatR;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Application.Features.Products.Queries.GetVariationById;
using StockMode.Domain.Products;

namespace StockMode.Application.Features.Products.Queries
{
    public class GetVariationByIdQueryHandler(IProductRepository repository) : IRequestHandler<GetVariationByIdQuery, VariationDetailDto>
    {
        public async Task<VariationDetailDto> Handle(GetVariationByIdQuery request, CancellationToken cancellationToken)
        {
            var variation = await repository.FindVariationByIdAsync(request.VariationId);

            if (variation is null)
                return null!;

            var variationDto = new VariationDetailDto(variation.Id,
                variation.Name,
                variation.Sku,
                variation.CostPrice,
                variation.SalePrice,
                variation.StockQuantity);

            return variationDto;
        }
    }
}
