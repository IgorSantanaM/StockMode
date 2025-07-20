using StockMode.Domain.Core.Data;
using StockMode.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Domain.Sales
{
    public interface ISaleRepository : IRepository<Sale, int>
    {
        Task<Sale?> GetSaleByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Sale>> GetSalesByStatusAsync(SaleStatus status);
        Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
