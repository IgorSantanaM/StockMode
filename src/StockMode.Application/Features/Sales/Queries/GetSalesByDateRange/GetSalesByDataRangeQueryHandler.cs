using MediatR;
using StockMode.Application.Exceptionns;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Domain.Products;
using StockMode.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Sales.Queries.GetSalesByDateRange
{
    public class GetSalesByDataRangeQueryHandler(ISaleRepository repository, IProductRepository productRepo) : IRequestHandler<GetSalesByDataRangeQuery, IReadOnlyCollection<SaleDetailsDto>>
    {
        public async Task<IReadOnlyCollection<SaleDetailsDto>> Handle(GetSalesByDataRangeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Sale?> sales = await repository.GetSalesByDateRangeAsync(request.StartDate, request.EndDate);
                
                if (sales is null)
                    throw new NotFoundException(nameof(Sale), request.StartDate);

                var variationId = sales.Select(sales => sales?.Items.Select(i => i.VariationId)).SelectMany(x => x).Distinct().ToList();

                var variations = await productRepo.GetVariationsWithProductsByIdsAsync(variationId);
                
                var variationNames = variations.ToDictionary(v => v.Id, v => $"{v.Product.Name} - {v.Name}");

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
                            variationNames.GetValueOrDefault(si.VariationId, "Unknown Product"),
                            si.VariationId,
                            si.Quantity,
                            si.PriceAtSale
                        )).ToList()
                    ))
                    .ToList();

                return salesDto;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null!;
            }
        }
    }
}
