using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Domain.Core.Data;
using StockMode.Domain.Products;

namespace StockMode.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await repository.GetProductByIdAsync(request.ProductId, cancellationToken);

            if(product is null)
                throw new NotFoundException(nameof(product), request.ProductId);

            repository.Delete(product);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
