using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Customers;
using StockMode.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustumerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCustomerCommandHandler(ICustumerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }
        public Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (request.CustomerId <= 0)
                throw new ArgumentException("Invalid customer ID.");

            var customer = _customerRepository.GetCustomerById(request.CustomerId).Result;

            if (customer! == null!)
                throw new NotFoundException(nameof(Customer), request.CustomerId);

            var address = new Address(request.AddressDto.Number, request.AddressDto.Street, request.AddressDto.City, request.AddressDto.State, request.AddressDto.ZipCode);

            customer.UpdateDetails(request.Name, request.Email, request.PhoneNumber, address);
            _customerRepository.Update(customer);
            return _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
