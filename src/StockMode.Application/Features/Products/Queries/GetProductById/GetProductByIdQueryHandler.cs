using MediatR;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Domain.Products;

namespace StockMode.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
    {

        public async Task<ProductDetailsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await repository.GetProductByIdAsync(request.ProductId, cancellationToken);

            if (product is null)
                return null!;

            var productDto = new ProductDetailsDto(
                product.Id,
                product.Name,
                product.Description,
                product.IsActive,
                product.Variations.Select(v => new VariationDetailDto(
                    v.Id,
                    v.Name,
                    v.Sku,
                    v.CostPrice,
                    v.SalePrice,
                    v.StockQuantity
                )).ToList()
            );
            return productDto;
        }
    }
}
