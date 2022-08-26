namespace Application.Models;

/// <summary>
///   Base class for all entities that have Id and timestamps
/// </summary>
public abstract class BaseEntity
{
    public int? Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public BaseEntity(DateTime createdAt)
    {
        CreatedAt = createdAt;
    }
}
