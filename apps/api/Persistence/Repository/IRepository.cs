using System.Linq.Expressions;

namespace Persistence.Repository;

public interface IRepository<TEntity>
{
    Task<TEntity> Save(TEntity entity);
    Task<TEntity> FindOne(int id);
    Task<ICollection<TEntity>> FindMany(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy
    );
    Task<ICollection<TEntity>> FindMany(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        IPageable pageable
    );
    Task DeleteOne(int id);
}
