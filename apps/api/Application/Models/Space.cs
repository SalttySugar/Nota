namespace Application.Models;

public class Space : BaseEntity
{
    public string Name { get; set; }
    public virtual Workspace Workspace { get; set; }
    public virtual ICollection<Note> Notes { get; set; } = new HashSet<Note>();

    public Space(string name, Workspace workspace, DateTime createdAt) : base(createdAt)
    {
        Name = name;
        Workspace = workspace;
    }
}
