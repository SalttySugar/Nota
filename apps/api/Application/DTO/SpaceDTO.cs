namespace Application.DTO;

public class SpaceDTO : BaseDTO
{
    public string Name { get; set; }
    public ICollection<int> NotesIds { get; set; }
    public int WorkspaceId { get; set; }
}
