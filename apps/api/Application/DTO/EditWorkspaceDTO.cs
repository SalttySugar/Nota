namespace Application.DTO;

public class EditWorkspaceDTO
{
    public string Name { get; set; }

    public EditWorkspaceDTO(string name)
    {
        Name = name;
    }
}
