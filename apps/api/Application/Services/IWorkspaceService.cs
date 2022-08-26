using Application.DTO;
using Application.Models;

namespace Application.Services;

public interface IWorkspaceService : ICrudService<Workspace, EditWorkspaceDTO, EditWorkspaceDTO> { }
