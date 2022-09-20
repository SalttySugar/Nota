namespace Application.Models;

public class Space : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual Workspace Workspace { get; set; } = null!;
    public virtual ICollection<Note> Notes { get; set; } = new HashSet<Note>();
}