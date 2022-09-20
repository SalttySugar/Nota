using Application.Core.UseCases;
using Application.Errors;
using Application.Infrastructure.Persistence;
using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

namespace Application.Core.Services;

public class SpaceService : ISpaceCrudUseCase
{
    private readonly NotaContext _context;

    public SpaceService(NotaContext context)
    {
        _context = context;
    }

    public async Task<Space> FindOne(int id)
    {
        var space = await _context.Spaces.FindAsync(id);

        if (space == null)
        {
            throw new EntityNotFoundByIdException<int, Space>(id);
        }

        return space;
    }

    public async Task<ICollection<Space>> FindMany()
    {
        return await _context.Spaces.ToListAsync();
    }

    public async Task<Space> CreateOne(CreateSpaceArguments arguments)
    {
        var workspace = await _context.Workspaces.FindAsync(arguments.WorkspaceId);

        if (workspace == null)
        {
            throw new EntityNotFoundByIdException<int, Workspace>(arguments.WorkspaceId);
        }

        var space = _context.Spaces.Add(new Space
        {
            Name = arguments.Name,
            Workspace = workspace
        });

        await _context.SaveChangesAsync();
        return space.Entity;
    }

    public async Task<Space> UpdateOne(UpdateSpaceArguments arguments, int id)
    {
        var space = await FindOne(id);
        var workspaceId = arguments.WorkspaceId;
        var name = arguments.Name;
        var isChanged = false;

        if (name != null)
        {
            space.Name = name;
            isChanged = true;
        }

        if (workspaceId != null)
        {
            var workspace = await _context.Workspaces.FindAsync(workspaceId);

            if (workspace == null)
            {
                throw new EntityNotFoundByIdException<int, Workspace>((int) workspaceId);
            }

            space.Workspace = workspace;
            isChanged = true;
        }

        if (!isChanged) return space;

        space.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();

        return space;
    }

    public async Task DeleteOne(int id)
    {
        var space = await FindOne(id);
        _context.Remove(space);
        await _context.SaveChangesAsync();
    }

    public Task<int> Count()
    {
        return _context.Spaces.CountAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var result = await _context.Spaces.FindAsync(id);
        return result is not null;
    }
}