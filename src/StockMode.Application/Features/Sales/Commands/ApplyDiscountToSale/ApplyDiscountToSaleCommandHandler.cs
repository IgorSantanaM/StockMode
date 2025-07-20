using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Sales.Commands.ApplyDiscountToSale
{
    public class ApplyDiscountToSaleCommandHandler : IRequestHandler<ApplyDiscountToSaleCommand>
    {
        private readonly ISaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public ApplyDiscountToSaleCommandHandler(ISaleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ApplyDiscountToSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetSaleByIdAsync(request.SaleId);
            if(sale is null)
                throw new NotFoundException(nameof(Sale), request.SaleId);

            sale.ApplyDiscount(request.DiscountAmount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
