using MediatR;

namespace StockMode.Application.Features.Sales.Commands.DeleteSale;

public record DeleteSaleCommand(int SaleId) : IRequest;
