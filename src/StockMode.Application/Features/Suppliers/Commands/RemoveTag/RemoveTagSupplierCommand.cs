using MediatR;

namespace StockMode.Application.Features.Suppliers.Commands.RemoveTag;

public record RemoveTagSupplierCommand(int SupplierId, int TagId) : IRequest;

