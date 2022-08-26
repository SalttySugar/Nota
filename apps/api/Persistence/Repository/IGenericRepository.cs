using System.Linq.Expressions;

namespace Persistence.Repository;

public interface IGenericRepository<TEntity>
{
    Task<TEntity> Save(TEntity entity);
    Task<TEntity> FindOne(int id);
    Task<ICollection<TEntity>> FindMany(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy
    );
    Task DeleteOne(int id);
}
