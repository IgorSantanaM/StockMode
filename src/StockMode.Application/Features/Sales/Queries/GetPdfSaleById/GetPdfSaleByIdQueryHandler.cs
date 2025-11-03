using MediatR;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Exceptionns;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Application.PDF;
using StockMode.Domain.Customers;
using StockMode.Domain.Sales;

namespace StockMode.Application.Features.Sales.Queries.GetPdfSaleById
{
    public class GetPdfSaleByIdQueryHandler(ISaleRepository saleRepository, ICustomerRepository customerRepository, IPdfMaker pdfMaker) : IRequestHandler<GetPdfSaleByIdQuery, byte[]>
    {
        public async Task<byte[]> Handle(GetPdfSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await saleRepository.GetSaleByIdAsync(request.Id, cancellationToken);
            if(sale is null)
                throw new NotFoundException("Sale not found with id: ", request.Id);

            var customer = await customerRepository.GetCustomerById(sale.CustomerId);

            if (customer is null)
                return null!;

            var saleItems = sale.Items.Select(si => new SaleItemDetailsDto(
                si.Id,
                si.Variation.Name,
                si.VariationId,
                si.Quantity,
                si.PriceAtSale)).ToList();

            var saleModel = new SaleCompletedEmail(request.Id, customer.Email, sale.TotalPrice, sale.Discount, sale.FinalPrice, sale.PaymentMethod, sale.SaleDate, saleItems);

            var pdfBytes = pdfMaker.CreatePdfGeneric(saleModel, SaleCompletedRenderer.DrawSaleCompletedDetails);

            return pdfBytes;
        }
    }
}
