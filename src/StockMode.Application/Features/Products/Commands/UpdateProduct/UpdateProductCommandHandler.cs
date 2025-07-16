using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Products;

namespace StockMode.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand, ProductDetailsDto>
    {
        public async Task<ProductDetailsDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await repository.GetProductByIdAsync(request.ProductId, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }

            product.UpdateDetails(request.Name, request.Description);

            var variationsToRemove = product.Variations
                .Where(v => !request.Variations.Any(dto => dto.Sku == v.Sku))
                .ToList();

            foreach (var variation in variationsToRemove)
            {
                product.RemoveVariation(variation);
            }

            foreach (var dto in request.Variations)
            {
                var existingVariation = product.Variations.FirstOrDefault(v => v.Sku == dto.Sku);

                if (existingVariation is not null)
                {
                    existingVariation.Update(dto.Name, dto.Sku, dto.CostPrice, dto.SalePrice, dto.StockQuantity);
                }
                else
                {
                    product.AddVariation(dto.Name, dto.Sku, dto.CostPrice, dto.SalePrice, dto.StockQuantity);
                }
            }

            repository.Update(product);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new ProductDetailsDto(
                product.Id,
                product.Name,
                product.Description,
                product.IsActive,
                product.Variations.Select(v => new VariationDetailDto(
                    v.Id, v.Name, v.Sku, v.CostPrice, v.SalePrice, v.StockQuantity
                )).ToList()
            );
        }
    }
}
