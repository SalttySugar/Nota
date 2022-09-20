using Application.Core.UseCases.Generic;
using Application.Models;

namespace Application.Core.UseCases;

public interface ISpaceCrudUseCase : ICrudUseCase<int, Space, CreateSpaceArguments, UpdateSpaceArguments>
{
}

public record CreateSpaceArguments(string Name, int WorkspaceId);

public record UpdateSpaceArguments(string? Name, int? WorkspaceId);