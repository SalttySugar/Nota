using Application.Core.UseCases.Generic;
using Application.Models;

namespace Application.Core.UseCases;

public interface
    IWorkspaceCrudUseCase : ICrudUseCase<int, Workspace, CreateWorkspaceArguments, UpdateWorkspaceArguments>
{
}

public record CreateWorkspaceArguments(string Name);

public record UpdateWorkspaceArguments(string? Name);