namespace Application.Models;

public class Note : BaseEntity
{
    public string Title { get; set; }
    public string? Content { get; set; }
    public virtual Space Space { get; set; }

    public Note(string title, Space space, DateTime createdAt) : base(createdAt)
    {
        Title = title;
        Space = space;
    }
}
