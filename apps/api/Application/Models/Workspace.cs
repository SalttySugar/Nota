namespace Application.Models;

public class Workspace : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Space> Spaces { get; set; } = new HashSet<Space>();

    public Workspace(string name, DateTime dateTime) : base(dateTime)
    {
        Name = name;
    }
}
