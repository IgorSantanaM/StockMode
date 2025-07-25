using StockMode.Domain.Sales.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Common.Interfaces
{
    public interface IMailer
    {
        Task SendSaleCompletedAsync(SaleCompletedEvent saleCompletedEvent, CancellationToken token);
    }
}
