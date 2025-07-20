using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Sales.Queries.GetSalesByDateRange
{
    public class GetSalesByDataRangeQueryHandler(ISaleRepository repository) : IRequestHandler<GetSalesByDataRangeQuery, IReadOnlyCollection<SaleDetailsDto>>
    {
        public async Task<IReadOnlyCollection<SaleDetailsDto>> Handle(GetSalesByDataRangeQuery request, CancellationToken cancellationToken)
        {
            var sales = await repository.GetSalesByDateRangeAsync(request.StartDate, request.EndDate);

            if (sales is null)
                throw new NotFoundException(nameof(Sale), request.StartDate);

            var salesDto = sales
                .Select(sale => new SaleDetailsDto(
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
                        si.PriceAtSale
                    )).ToList()
                ))
                .ToList();

            return salesDto;
        }
    }
}
