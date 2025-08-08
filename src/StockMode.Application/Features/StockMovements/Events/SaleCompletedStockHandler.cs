using MediatR;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Products;
using StockMode.Domain.Sales.Events;
using StockMode.Domain.StockMovements;

namespace StockMode.Application.Features.StockMovements.Events
{
    public class SaleCompletedStockHandler : INotificationHandler<SaleCompletedEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SaleCompletedStockHandler(
            IProductRepository productRepository,
            IStockMovementRepository stockMovementRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _stockMovementRepository = stockMovementRepository ?? throw new ArgumentNullException(nameof(stockMovementRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(SaleCompletedEvent notification, CancellationToken cancellationToken)
        {
            foreach (var soldItem in notification.SoldItems)
            {
                var product = await _productRepository.GetProductByVariationIdAsync(soldItem.VariationId);
                var variation = product?.Variations.FirstOrDefault(v => v.Id == soldItem.VariationId);
                if (variation! != null!)
                {
                    variation.DecreaseStock(soldItem.Quantity);

                    StockMovement? movement = StockMovement.CreateForSale(
                        variation.Id,
                        soldItem.Quantity,
                        variation.StockQuantity,
                        notification.SaleId,
                        notification.CustomerId);

                    await _stockMovementRepository.AddAsync(movement!);
                }
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
