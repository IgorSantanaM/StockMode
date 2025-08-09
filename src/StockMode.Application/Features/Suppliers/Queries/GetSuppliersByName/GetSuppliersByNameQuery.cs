using MediatR;
using StockMode.Application.Features.Suppliers.Dtos;

namespace StockMode.Application.Features.Suppliers.Queries.GetSuppliersByName;

public record GetSuppliersByNameQuery(string Name) : IRequest<IEnumerable<SupplierSummaryDto>>;
