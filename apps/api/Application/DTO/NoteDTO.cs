namespace Application.DTO;

public class NoteDTO : BaseDTO
{
    public virtual string Title { get; set; } = default!;
    public virtual string Content { get; set; } = default!;
    public virtual int SpaceId { get; set; }

}
