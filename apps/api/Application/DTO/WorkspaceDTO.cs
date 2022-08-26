namespace Application.DTO;

public class WorkspaceDTO : BaseDTO
{
    public string Name { get; set; }
    public ICollection<int> SpacesIds { get; set; }

    public WorkspaceDTO(int id, string name, ICollection<int> spacesIds, DateTime createdAt)
        : base(id, createdAt)
    {
        Name = name;
        SpacesIds = spacesIds;
    }
}
