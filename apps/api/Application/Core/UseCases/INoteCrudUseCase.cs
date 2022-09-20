using Application.Core.UseCases.Generic;
using Application.Models;

namespace Application.Core.UseCases;

public interface INoteCrudUseCase : ICrudUseCase<int, Note, CreateNoteArguments, UpdateNoteArguments>
{
}

public record CreateNoteArguments(string Title, string? Content, int SpaceId);

public record UpdateNoteArguments(string? Title, string? Content, int? SpaceId);