using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Suppliers;

namespace StockMode.Application.Features.Suppliers.Commands.DeleteSupplier
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSupplierCommandHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.GetSupplierById(request.Id);

            if(supplier is null)
                throw new NotFoundException(nameof(Supplier), request.Id);

            _supplierRepository.Delete(supplier);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
