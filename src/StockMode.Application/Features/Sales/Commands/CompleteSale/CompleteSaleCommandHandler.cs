using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Sales;

namespace StockMode.Application.Features.Sales.Commands.CompleteSale
{
    public class CompleteSaleCommandHandler : IRequestHandler<CompleteSaleCommand>
    {
        private readonly ISaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public CompleteSaleCommandHandler(ISaleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CompleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetSaleByIdAsync(request.SaleId);
            if (sale is null)
                throw new NotFoundException(nameof(Sale), request.SaleId);

            sale.CompleteSale(request.CustomerId);

            _repository.Update(sale);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
