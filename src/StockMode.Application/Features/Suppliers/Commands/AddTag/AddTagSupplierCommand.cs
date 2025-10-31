using MediatR;

namespace StockMode.Application.Features.Suppliers.Commands.AddTag;

public record AddTagSupplierCommand(int SupplierId, int TagId) : IRequest;
