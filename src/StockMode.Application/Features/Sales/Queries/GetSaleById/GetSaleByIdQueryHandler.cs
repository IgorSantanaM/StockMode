using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Sales;

namespace StockMode.Application.Features.Sales.Queries.GetSaleById
{
    public class GetSaleByIdQueryHandler(ISaleRepository repository) : IRequestHandler<GetSaleByIdQuery, SaleDetailsDto>
    {
        public async Task<SaleDetailsDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await repository.GetSaleByIdAsync(request.SaleId, cancellationToken);

            if (sale is null)
               throw new NotFoundException(nameof(Sale), request.SaleId);

            var saleDto = new SaleDetailsDto(
                sale.Id,
                sale.SaleDate,
                sale.TotalPrice,
                sale.Discount,
                sale.FinalPrice,
                sale.PaymentMethod.ToString(),
                sale.Status.ToString(),
                sale.Items.Select(si => new SaleItemDetailsDto(
                    si.Id,
                    si.VariationId,
                    si.Quantity,
                    si.PriceAtSale)).ToList());

            return saleDto;
        }
    }
}
