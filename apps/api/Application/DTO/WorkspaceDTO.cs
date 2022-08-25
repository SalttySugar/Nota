namespace Application.DTO;

public class WorkspaceDTO : BaseDTO
{
    public string Name { get; set; }
    public ICollection<int> SpacesIds { get; set; }
}
