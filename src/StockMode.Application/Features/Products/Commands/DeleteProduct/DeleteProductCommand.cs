using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int ProductId) : IRequest<Unit>;
