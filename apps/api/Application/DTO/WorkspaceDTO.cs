namespace Application.DTO;

public class WorkspaceDTO : BaseDTO
{
    public virtual string Name { get; set; } = null!;
    public virtual ICollection<int> SpacesIds { get; set; } = null!;
}
