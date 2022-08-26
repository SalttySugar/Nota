namespace Application.DTO;

public abstract class BaseDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public BaseDTO(int id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }
}
