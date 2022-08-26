namespace Application.DTO;

public class CreateNoteDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int SpaceId { get; set; }

    public CreateNoteDTO(string title, string content, int spaceId)
    {
        Title = title;
        Content = content;
        SpaceId = spaceId;
    }
}
