using MediatR;
using StockMode.Application.Features.Suppliers.Dtos;
using StockMode.Domain.Common;

namespace StockMode.Application.Features.Suppliers.Queries.GetAllSuppliers;

public record GetAllSuppliersQuery(string? Name, int Page, int PageSize) : IRequest<PagedResult<SupplierSummaryDto>>;
