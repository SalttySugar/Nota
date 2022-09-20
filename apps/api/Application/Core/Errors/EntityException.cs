namespace Application.Errors;

public abstract class EntityException : Exception
{
    public EntityException(string message) : base(message)
    {
    }
}