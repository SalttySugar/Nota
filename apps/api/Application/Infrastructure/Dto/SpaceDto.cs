namespace Application.Infrastructure.Dto;

public record SpaceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<int> NotesIds = new HashSet<int>();
    public int WorkspaceId { get; set; }
    public DateTime CreatedAt { get; set; } = default!;
    public DateTime? UpdatedAt { get; set; }
}