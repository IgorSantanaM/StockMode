using MediatR;
using StockMode.Application.Features.StockMovements.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.StockMovements.Commands.ReceivePurchaseOrder
{
    public record ReceivePurchaseOrderCommand(IReadOnlyCollection<ReceivedItemsDto> Items, int SupplierId) : IRequest;
}
