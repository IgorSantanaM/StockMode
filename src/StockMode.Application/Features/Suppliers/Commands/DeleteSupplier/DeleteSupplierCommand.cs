using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Suppliers.Commands.DeleteSupplier;

public record DeleteSupplierCommand(int Id) : IRequest; 
