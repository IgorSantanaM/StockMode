using StockMode.Domain.Core.Data;

namespace StockMode.Domain.StockMovements
{
    public interface IStockMovementRepository : IRepository<StockMovement, int>
    {
        Task<IEnumerable<StockMovement>> GetMovementsByVariationIdAsync(int variationId);
        Task<IEnumerable<StockMovement>> GetMovementsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
