﻿using StockMode.Domain.Core.Model;
using System.Linq.Expressions;

namespace StockMode.Domain.Core.Data
{
    public interface IRepository<TEntity, out TId> where TEntity : IAggregateRoot
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
