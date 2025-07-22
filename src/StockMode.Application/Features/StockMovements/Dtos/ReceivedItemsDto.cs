using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.StockMovements.Dtos
{
    public record ReceivedItemsDto(int VariationId, int QuantityReceived);
}
