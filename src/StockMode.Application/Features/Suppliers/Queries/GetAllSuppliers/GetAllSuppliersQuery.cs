using MediatR;
using StockMode.Application.Features.Suppliers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Suppliers.Queries.GetAllSuppliers;

public record GetAllSuppliersQuery : IRequest<IEnumerable<SupplierSummaryDto>>;
