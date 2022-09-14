namespace Application.Errors;
public class WorkspaceNotFoundById : Exception
{
    public WorkspaceNotFoundById(int id) : base($"Could not find Note with id: {id}") { }
}