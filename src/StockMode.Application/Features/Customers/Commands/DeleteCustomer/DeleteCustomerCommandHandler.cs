using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustumerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCustomerCommandHandler(ICustumerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            if (request.CustomerId <= 0)
                throw new ArgumentException("Invalid customer ID.");

            var customer = await _customerRepository.GetCustomerById(request.CustomerId);

            if (customer! == null!)
                throw new NotFoundException(nameof(Customer), request.CustomerId);

            _customerRepository.Delete(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
