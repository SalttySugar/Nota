namespace Application.Models;

public class Workspace : BaseEntity
{
    public virtual string Name { get; set; } = "";
    public virtual ICollection<Space> Spaces { get; set; } = new HashSet<Space>();

}
