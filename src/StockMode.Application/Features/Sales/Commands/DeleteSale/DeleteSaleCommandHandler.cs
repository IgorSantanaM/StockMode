using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Sales;

namespace StockMode.Application.Features.Sales.Commands.DeleteSale
{
    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand>
    {
        private readonly ISaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSaleCommandHandler(ISaleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetSaleByIdAsync(request.SaleId, cancellationToken);

            if (sale is null)
                throw new NotFoundException(nameof(Sale), request.SaleId);

            _repository.Delete(sale);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
