using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Suppliers;
using StockMode.Domain.ValueObjects;

namespace StockMode.Application.Features.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateSupplierCommandHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.GetSupplierById(request.Id);

            if (supplier is null)
                throw new NotFoundException(nameof(Supplier), request.Id);

            var address = new Address(request.AddressDto.Number, request.AddressDto.Street, request.AddressDto.City, request.AddressDto.State, request.AddressDto.ZipCode);

            supplier.UpdateDetails(request.Name, request.CorporateName, request.CNPJ, request.ContactPerson, request.Email, request.PhoneNumber, address, request.Notes);

            if (request.TagIds is not null)
            {
                supplier.SetTags(request.TagIds);
            }
            else
                supplier.ClearTags();

            _supplierRepository.Update(supplier);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
