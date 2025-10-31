using StockMode.Domain.Core.Data;

namespace StockMode.Domain.Tags
{
    public interface ITagRepository : IRepository<Tag, int>
    {
        public Task<Tag?> GetTagById(int id);

    }
}
