using FluentValidation;
using MediatR;
using StockMode.Application.Features.Sales.Commands.CreateSale;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Core.Exceptions;
using StockMode.Domain.Products;
using StockMode.Domain.Sales;

namespace StockMode.Application.Features.Sales.Commands.CreateSaleItem
{
    public class AddItemToSaleCommandHandler : IRequestHandler<AddItemToSaleCommandDto>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddItemToSaleCommandDto> _validator;

        public AddItemToSaleCommandHandler(
            ISaleRepository saleRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IValidator<AddItemToSaleCommandDto> validator)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task Handle(AddItemToSaleCommandDto request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var variation = await _productRepository.FindVariationByIdAsync(request.VariationId);
            if (variation is null) throw new DomainException($"Variation not found with id '{request.VariationId}'");

            var sale = await _saleRepository.GetSaleByIdAsync(request.SaleId);
            if (sale is null) throw new DomainException($"Sale not found with id '{request.SaleId}'");

            var newItem = new SaleItem(variation.Id, request.Quantity, variation.SalePrice);

            sale.AddItem(newItem);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
