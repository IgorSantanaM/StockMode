using MediatR;

namespace StockMode.Application.Features.Sales.Commands.CompleteSale;

public record CompleteSaleCommand(int SaleId, int CustomerId) : IRequest;
