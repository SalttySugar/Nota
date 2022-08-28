namespace Application.DTO;

public class UpdateNoteDTO
{
    public virtual string? Title { get; set; }
    public virtual string? Content { get; set; }
    public virtual int? SpaceId { get; set; }
}
