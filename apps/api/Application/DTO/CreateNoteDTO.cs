namespace Application.DTO;

public class CreateNoteDTO
{
    public virtual string Title { get; set; } = null!;
    public virtual string Content { get; set; } = default!;
    public virtual int SpaceId { get; set; }
}
