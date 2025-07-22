using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.StockMovements.Commands.AdjustStock
{
    public record AdjustStockCommand(int VariationId, int QuantityAdjusted, string Reason) : IRequest;
}
