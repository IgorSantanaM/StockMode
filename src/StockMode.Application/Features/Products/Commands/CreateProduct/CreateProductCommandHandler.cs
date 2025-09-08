using FluentValidation;
using MediatR;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Products;
using System.Runtime.InteropServices;

namespace StockMode.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProductCommand> _validator;
        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IValidator<CreateProductCommand> validator)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            foreach (var variationDto in request.Variations)
            {
                if (await _productRepository.GetProductBySkuAsync(variationDto.Sku) != null)
                {
                    throw new ArgumentException($"Variation with SKU '{variationDto.Sku}' already exists.");
                }
            }

            var product = new Product(request.Name, request.Description);

            foreach (var variationDto in request.Variations)
            {
                product.AddVariation(
                    variationDto.Name,
                    variationDto.Sku,
                    variationDto.CostPrice,
                    variationDto.SalePrice,
                    variationDto.StockQuantity);
            }

            product.AddToDomainEvent();

            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
