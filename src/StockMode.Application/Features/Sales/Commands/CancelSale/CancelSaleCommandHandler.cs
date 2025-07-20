using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Sales;

namespace StockMode.Application.Features.Sales.Commands.CancelSale
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand>
    {
        private readonly ISaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public CancelSaleCommandHandler(ISaleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetSaleByIdAsync(request.SaleId);

            if (sale is null)
                throw new NotFoundException(nameof(Sale), request.SaleId);

            sale.CancelSale();

            _repository.Update(sale);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
