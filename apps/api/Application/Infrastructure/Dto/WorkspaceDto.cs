namespace Application.Infrastructure.Dto;

public class WorkspaceDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<int> SpacesIds { get; set; } = new HashSet<int>();

    public DateTime CreatedAt { get; set; } = default!;
    public DateTime? UpdatedAt { get; set; }
}