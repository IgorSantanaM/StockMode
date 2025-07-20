using MediatR;

namespace StockMode.Application.Features.Sales.Commands.ApplyDiscountToSale;

public record ApplyDiscountToSaleCommand(int SaleId,
    decimal DiscountAmount) : IRequest;
