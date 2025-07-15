using MediatR;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Domain.Products;

namespace StockMode.Application.Features.Products.Queries.GetProductBySku
{
    public class GetProductBySkuQueryHandler(IProductRepository repository) : IRequestHandler<GetProductBySkuQuery, ProductDetailsDto>
    {
        public async Task<ProductDetailsDto> Handle(GetProductBySkuQuery request, CancellationToken cancellationToken)
        {
            var product = await repository.GetProductBySkuAsync(request.Sku);

            if(product is null)
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
