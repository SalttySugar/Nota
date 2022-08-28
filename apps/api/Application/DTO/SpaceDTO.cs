namespace Application.DTO;

public class SpaceDTO : BaseDTO
{
    public virtual string Name { get; set; } = default!;
    public virtual ICollection<int> NotesIds { get; set; } = new HashSet<int>();
    public virtual int WorkspaceId { get; set; }
}
