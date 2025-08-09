using FluentValidation;
using MediatR;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Suppliers;
using StockMode.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, int>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateSupplierCommand> _validator;
        public CreateSupplierCommandHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork, IValidator<CreateSupplierCommand> validator)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<int> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var address = new Address(request.AddressDto.Number, request.AddressDto.Street, request.AddressDto.City, request.AddressDto.State, request.AddressDto.ZipCode);

            var supplier = new Supplier(request.Name, request.CorporateName, request.CNPJ, request.ContatctPerson, request.Email, request.PhoneNumber, address);

            await _supplierRepository.AddAsync(supplier);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return supplier.Id;
        }
    }
}
