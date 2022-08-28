namespace Application.DTO;

public class WorkspaceDTO : BaseDTO
{
    public string Name { get; set; } = null!;
    public ICollection<int> SpacesIds { get; set; } = null!;
}
