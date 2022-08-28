namespace Application.DTO;

public class CreateNoteDTO
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = default!;
    public int SpaceId { get; set; }
}
