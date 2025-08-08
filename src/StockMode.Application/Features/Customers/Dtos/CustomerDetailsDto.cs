using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Customers.Dtos
{
    public record CustomerDetailsDto(
        int Id,
        string Name,
        string Email,
        string PhoneNumber,
        string? Street,
        string? City,
        string? State,
        string? ZipCode,
        DateTime? LastPurchase,
        decimal TotalSpent
    );
}
