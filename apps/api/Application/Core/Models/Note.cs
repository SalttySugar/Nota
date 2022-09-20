namespace Application.Models;

public class Note : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Content { get; set; }
    public virtual Space Space { get; set; } = null!;
}