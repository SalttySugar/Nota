using Application.Core.UseCases;
using Application.Errors;
using Application.Infrastructure.Persistence;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.Services;

public class WorkspaceService : IWorkspaceCrudUseCase
{
    public WorkspaceService(NotaContext context)
    {
        _context = context;
    }

    private readonly NotaContext _context;

    public async Task<Workspace> FindOne(int id)
    {
        var workspace = await _context.Workspaces.FindAsync(id);

        if (workspace == null)
        {
            throw new EntityNotFoundByIdException<int, Workspace>(id);
        }

        return workspace;
    }

    public async Task<ICollection<Workspace>> FindMany()
    {
        return await _context.Workspaces.ToListAsync();
    }

    public async Task<Workspace> CreateOne(CreateWorkspaceArguments arguments)
    {
        var workspace = _context.Workspaces.Add(new Workspace
        {
            CreatedAt = DateTime.Now,
            Name = arguments.Name
        });
        await _context.SaveChangesAsync();
        return workspace.Entity;
    }

    public async Task<Workspace> UpdateOne(UpdateWorkspaceArguments arguments, int id)
    {
        var workspace = await FindOne(id);
        var isEdited = false;

        if (arguments.Name != null)
        {
            workspace.Name = arguments.Name;
            isEdited = true;
        }

        if (isEdited)
        {
            workspace.UpdatedAt = DateTime.Now;
            _context.Workspaces.Update(workspace);
            await _context.SaveChangesAsync();
        }

        return workspace;
    }

    public async Task DeleteOne(int id)
    {
        var workspace = await FindOne(id);
        _context.Workspaces.Remove(workspace);
        await _context.SaveChangesAsync();
    }

    public async Task<int> Count()
    {
        return await _context.Workspaces.CountAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var result = await _context.Workspaces.FindAsync(id);
        return result is not null;
    }
}