namespace Application.DTO;

public class CreateSpaceDTO
{
    public string Name { get; set; }
    public int WorkspaceId { get; set; }

    public CreateSpaceDTO(string name, int workspaceId)
    {
        Name = name;
        WorkspaceId = workspaceId;
    }
}
