using MediatR;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Domain.Products;

namespace StockMode.Application.Features.Products.Queries.GetProductsWithLowStock
{
    public class GetProductsWithLowStockQueryHandler(IProductRepository repository) : IRequestHandler<GetProductsWithLowStockQuery, IEnumerable<ProductDetailsDto>>
    {
        public async Task<IEnumerable<ProductDetailsDto>> Handle(GetProductsWithLowStockQuery request, CancellationToken cancellationToken)
        {
            var product = await repository.GetActiveProductsWithLowStockAsync(request.LowStockThreshold);

            if (product is null || !product.Any())
                return null!;

            var productDtos = product.Select(p => new ProductDetailsDto(
                p.Id,
                p.Name,
                p.Description,
                p.IsActive,
                p.Variations.Select(v => new VariationDetailDto(
                    v.Id,
                    v.Name,
                    v.Sku,
                    v.CostPrice,
                    v.SalePrice,
                    v.StockQuantity)).ToList()));

            return productDtos;
        }
    }
}
