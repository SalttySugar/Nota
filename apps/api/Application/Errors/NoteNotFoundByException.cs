namespace Application.Errors;

public class NoteNotFoundException : Exception
{
    public NoteNotFoundException(int id) : base($"Could not find Note with id: {id}") { }
}
