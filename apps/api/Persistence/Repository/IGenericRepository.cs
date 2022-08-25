using System.Linq.Expressions;

namespace Persistence.Repository;

public interface IGenericRepository<TId, TEntity>
{
    TEntity Save(TEntity entity);
    TEntity FindOne(TEntity entity);
    TEntity FindMany(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy
    );
    TEntity DeleteOne(TEntity entity);
}
