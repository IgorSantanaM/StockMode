using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Sales;

namespace StockMode.Application.Features.Sales.Commands.ChangeSalePaymentMethod
{
    public class ChangeSalePaymentMethodCommandHandler : IRequestHandler<ChangeSalePaymentMethodCommand>
    {
        private readonly ISaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeSalePaymentMethodCommandHandler(ISaleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ChangeSalePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetSaleByIdAsync(request.SaleId);
            if (sale is null)
                throw new NotFoundException(nameof(Sale), request.SaleId);

            sale.ChangePaymentMethod(request.NewPaymentMethod);

            _repository.Update(sale);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
