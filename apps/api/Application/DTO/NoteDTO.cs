namespace Application.DTO;

public class NoteDTO : BaseDTO
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public int SpaceId { get; set; }

}
