using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Customers;
using StockMode.Domain.ValueObjects;

namespace StockMode.Application.Features.Suppliers.Commands.AddTag
{
    public class AddTagCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork) : IRequestHandler<AddTagCustomerCommand>
    {
        public async Task Handle(AddTagCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetCustomerById(request.CustomerId);

            if (customer is null)
                throw new NotFoundException($"Customer with id {request.CustomerId} was not found.", typeof(Customer));

            customer.AddTag(new TagId(request.TagId));
            customerRepository.Update(customer);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
