namespace Application.Models;

/// <summary>
///   Base class for all entities that have Id and timestamps
/// </summary>
public class BaseEntity
{
    public virtual int Id { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual DateTime? UpdatedAt { get; set; }

}
