using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Products;
using StockMode.Domain.StockMovements;

namespace StockMode.Application.Features.StockMovements.Commands.ReceivePurchaseOrder
{
    public class ReceivePurchaseOrderCommandHandler : IRequestHandler<ReceivePurchaseOrderCommand>
    {
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReceivePurchaseOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            IStockMovementRepository stockMovementRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _stockMovementRepository = stockMovementRepository ?? throw new ArgumentNullException(nameof(stockMovementRepository));
        }
        public async Task Handle(ReceivePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Items)
            {
                var variation = await _productRepository.FindVariationByIdAsync(item.VariationId);
                if (variation is null)
                    throw new NotFoundException(nameof(Variation), item.VariationId);

                variation.IncreaseStock(item.QuantityReceived);

                var movement = StockMovement.CreateForPurchase(
                    variation.Id,
                    item.QuantityReceived,
                    variation.StockQuantity);

                await _stockMovementRepository.AddAsync(movement);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
