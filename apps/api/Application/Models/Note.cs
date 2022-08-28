namespace Application.Models;

public class Note : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public virtual Space Space { get; set; } = null!;
}
