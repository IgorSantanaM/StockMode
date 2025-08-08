using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Customers.Dtos;

public record CustomerSummaryDto(string Name,
                                string Email,
                                string PhoneNumber,
                                DateTime? LastPurchase,
                                decimal TotalSpent);
