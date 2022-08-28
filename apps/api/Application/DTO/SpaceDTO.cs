namespace Application.DTO;

public class SpaceDTO : BaseDTO
{
    public string Name { get; set; } = default!;
    public ICollection<int> NotesIds { get; set; } = new HashSet<int>();
    public int WorkspaceId { get; set; }
}
