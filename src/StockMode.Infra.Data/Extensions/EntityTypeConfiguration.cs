using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StockMode.Infra.Data.Extensions
{
    public abstract class EntityTypeConfiguration<TEntity> :  IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
