using MediatR;

namespace StockMode.Application.Features.Sales.Commands.CancelSale;

public record CancelSaleCommand(int SaleId) : IRequest;
