using StockMode.Domain.Core.Data;
using StockMode.Infra.Data.Contexts;

namespace StockMode.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly StockModeContext _context;
        private bool _disposed;
        public UnitOfWork(StockModeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
