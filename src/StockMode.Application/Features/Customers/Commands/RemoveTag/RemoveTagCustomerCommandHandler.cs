using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Application.Features.Suppliers.Commands.AddTag;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Customers;
using StockMode.Domain.ValueObjects;

namespace StockMode.Application.Features.Suppliers.Commands.RemoveTag
{
    public class RemoveTagCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveTagCustomerCommand>
    {
        public async Task Handle(RemoveTagCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetCustomerById(request.CustomerId);  

            if(customer is null)
                throw new NotFoundException($"Customer with id {request.CustomerId} was not found.", typeof(Customer));

            customer.RemoveTag(new TagId(request.TagId));
            customerRepository.Update(customer);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
