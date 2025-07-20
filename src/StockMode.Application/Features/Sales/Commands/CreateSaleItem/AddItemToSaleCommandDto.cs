using MediatR;
using StockMode.Application.Features.Products.Dtos;

namespace StockMode.Application.Features.Sales.Commands.CreateSaleItem
{
    public record AddItemToSaleCommandDto(
        int SaleId,
        int VariationId,
        int Quantity) : IRequest;
}
