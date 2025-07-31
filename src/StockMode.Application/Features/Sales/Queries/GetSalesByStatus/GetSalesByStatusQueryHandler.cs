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

namespace StockMode.Application.Features.Sales.Queries.GetSalesByStatus
{
    public class GetSalesByStatusQueryHandler(ISaleRepository repository, IProductRepository productRepo) : IRequestHandler<GetSalesByStatusQuery, IReadOnlyCollection<SaleDetailsDto>>
    {
        public async Task<IReadOnlyCollection<SaleDetailsDto>> Handle(GetSalesByStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sales = await repository.GetSalesByStatusAsync(request.Status);

                if (sales is null || !sales.Any())
                    throw new NotFoundException(nameof(Sale), nameof(request.Status));

                var variationIds = sales.SelectMany(sale => sale.Items.Select(i => i.VariationId)).Distinct().ToList();
                var variations = await productRepo.GetVariationsWithProductsByIdsAsync(variationIds);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null!;
            }
        }
    }
}
