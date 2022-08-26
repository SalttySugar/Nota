using System.Linq.Expressions;

namespace Persistence.Repository;

public interface IGenericRepository<TEntity>
{
    TEntity Save(TEntity entity);
    TEntity FindOne(int id);
    TEntity FindMany(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy
    );
    TEntity DeleteOne(int id);
}
