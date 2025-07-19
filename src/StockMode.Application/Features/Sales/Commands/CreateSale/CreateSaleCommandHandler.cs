using FluentValidation;
using MediatR;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Sales;

namespace StockMode.Application.Features.Sales.Commands.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, int>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateSaleCommand> _validator;

        public CreateSaleCommandHandler(ISaleRepository saleRepository, IUnitOfWork unitOfWork, IValidator<CreateSaleCommand> validator)
        {
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var sale = new Sale(request.PaymentMethod);

            await _saleRepository.AddAsync(sale);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return sale.Id;
        }
        
    }
}
