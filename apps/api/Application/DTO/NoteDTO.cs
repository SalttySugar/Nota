namespace Application.DTO;

public class NoteDTO : BaseDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int SpaceId { get; set; }

    public NoteDTO(int id, string title, string content, int spaceId, DateTime createdAt)
        : base(id, createdAt)
    {
        Title = title;
        Content = content;
        SpaceId = spaceId;
    }
}
