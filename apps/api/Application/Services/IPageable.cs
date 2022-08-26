namespace Application.Services;

public interface IPageable
{
    public int Limit { get; set; }
    public int Offset { get; set; }
}
