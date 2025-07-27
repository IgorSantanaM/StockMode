using StockMode.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Sales.Dtos
{
    public record SaleCompletedEmail(decimal FinalPrice, PaymentMethod PaymentMethod, DateTime SaleDate, IReadOnlyCollection<SaleItemDetailsDto> Items);
}
