namespace Application.DTO;

public class SpaceDTO : BaseDTO
{
    public string Name { get; set; }
    public ICollection<int> NotesIds { get; set; }
    public int WorkspaceId { get; set; }

    public SpaceDTO(
        int id,
        string name,
        int workspaceId,
        ICollection<int> notesIds,
        DateTime createdAt
    ) : base(id, createdAt)
    {
        Name = name;
        WorkspaceId = workspaceId;
        NotesIds = notesIds;
    }
}
