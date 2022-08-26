namespace Application.Services;

public class Pageable : IPageable
{
    public int Limit { get; set; }
    public int Offset { get; set; }

    public Pageable()
    {
        Limit = 10;
        Offset = 0;
    }
}
