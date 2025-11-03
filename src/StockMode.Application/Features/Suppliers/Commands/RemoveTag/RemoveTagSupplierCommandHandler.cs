using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Suppliers;
using StockMode.Domain.ValueObjects;

namespace StockMode.Application.Features.Suppliers.Commands.RemoveTag
{
    public class RemoveTagSupplierCommandHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveTagSupplierCommand>
    {
        public async Task Handle(RemoveTagSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await supplierRepository.GetSupplierById(request.SupplierId);

            if (supplier is null)
                throw new NotFoundException($"Supplier with id {request.SupplierId} was not found.", typeof(Supplier));

            supplier.RemoveTag(request.TagId);
            supplierRepository.Update(supplier);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
