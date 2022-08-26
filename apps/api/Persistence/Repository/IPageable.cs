namespace Persistence.Repository;

public interface IPageable
{
    public int Limit { get; set; }
    public int Offset { get; set; }
}
