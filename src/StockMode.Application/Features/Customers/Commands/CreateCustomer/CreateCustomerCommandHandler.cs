using FluentValidation;
using MediatR;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Customers;
using StockMode.Domain.ValueObjects;

namespace StockMode.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustumerRepository _customerRepository;
        private readonly IValidator<CreateCustomerCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCustomerCommandHandler(ICustumerRepository customerRepository, IValidator<CreateCustomerCommand> validator, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var address = new Address(request.AddressDto.Number, request.AddressDto.Street, request.AddressDto.City, request.AddressDto.State, request.AddressDto.ZipCode);

            var constumer = new Customer(request.Name, request.Email, request.PhoneNumber, address);

            await _customerRepository.AddAsync(constumer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return constumer.Id;

        }
    }
}
