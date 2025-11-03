using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Suppliers;
using StockMode.Domain.ValueObjects;

namespace StockMode.Application.Features.Suppliers.Commands.AddTag
{
    public class AddTagSupplierCommandHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork) : IRequestHandler<AddTagSupplierCommand>
    {
        public async Task Handle(AddTagSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await supplierRepository.GetSupplierById(request.SupplierId);

            if (supplier is null)
                throw new NotFoundException($"Supplier with id {request.SupplierId} was not found.", typeof(Supplier));

            supplier.AddTag(request.TagId);
            supplierRepository.Update(supplier);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
