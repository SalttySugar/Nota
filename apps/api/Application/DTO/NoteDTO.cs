namespace Application.DTO;

public class NoteDTO : BaseDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int SpaceId { get; set; }
}
