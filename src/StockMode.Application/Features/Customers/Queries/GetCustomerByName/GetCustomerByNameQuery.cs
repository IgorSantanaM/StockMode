using MediatR;
using StockMode.Application.Features.Customers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Customers.Queries.GetCustomerByName;

public record GetCustomerByNameQuery(string Name) : IRequest<IEnumerable<CustomerSummaryDto>>;
