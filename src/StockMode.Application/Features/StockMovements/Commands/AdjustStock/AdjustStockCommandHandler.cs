using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Products;
using StockMode.Domain.StockMovements;
using System.Collections.Concurrent;
using System.Reflection.Metadata;

namespace StockMode.Application.Features.StockMovements.Commands.AdjustStock
{
    public class AdjustStockCommandHandler : IRequestHandler<AdjustStockCommand>
    {
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdjustStockCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, IStockMovementRepository stockMovementRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _productRepository = productRepository;
            _stockMovementRepository = stockMovementRepository;
        }
        public async Task Handle(AdjustStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByVariationIdAsync(request.VariationId);
            var variation = product?.Variations.FirstOrDefault(v => v.Id == request.VariationId);

            if (variation is null)
                throw new NotFoundException(nameof(Variation), request.VariationId);

            if(request.QuantityAdjusted > 0)
                variation.IncreaseStock(request.QuantityAdjusted);
            else
                variation.DecreaseStock(Math.Abs(request.QuantityAdjusted));

            var movement = StockMovement.CreateForAdjustment(
                variation.Id,
                request.QuantityAdjusted,
                variation.StockQuantity,
                request.Reason);

            await _stockMovementRepository.AddAsync(movement);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
