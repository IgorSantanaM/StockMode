using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Suppliers.Dtos
{
    public record SupplierSummaryDto(int Id, string Name, string ContactPerson, string Email, string PhoneNumber)
    {
    }
}
