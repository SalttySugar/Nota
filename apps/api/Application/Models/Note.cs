namespace Application.Models;

public class Note : BaseEntity
{
    public virtual string Title { get; set; } = default!;
    public virtual string Content { get; set; } = default!;
    public virtual Space Space { get; set; } = null!;
}
