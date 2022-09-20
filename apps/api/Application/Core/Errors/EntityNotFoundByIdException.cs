namespace Application.Errors;

public class EntityNotFoundByIdException<TId, TEntity> : EntityException
{
    public int Id { get; protected set; }

    public EntityNotFoundByIdException(TId id)
        : base($"Could not find entity [{typeof(TEntity).Name}] with id: {id}")
    {
    }
}