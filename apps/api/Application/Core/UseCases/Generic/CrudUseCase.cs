namespace Application.Core.UseCases.Generic;

public interface ICrudUseCase<in TId, TEntity, in TCreateArguments, in TUpdateArguments> where TEntity : class
{
    Task<TEntity> FindOne(TId id);

    Task<ICollection<TEntity>> FindMany();

    Task<TEntity> CreateOne(TCreateArguments arguments);

    Task<TEntity> UpdateOne(TUpdateArguments arguments, TId id);

    Task DeleteOne(TId id);

    Task<int> Count();

    Task<bool> Exists(TId id);
}