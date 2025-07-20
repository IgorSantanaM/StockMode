using MediatR;
using StockMode.Application.Features.Products.Dtos;

namespace StockMode.Application.Features.Sales.Dtos
{
    public record AddItemToSaleCommandDto(
        int SaleId,
        int VariationId,
        int Quantity) : IRequest;
}
