using Application.Core.UseCases;
using Application.Errors;
using Application.Infrastructure.Persistence;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.Services;

public class NoteService : INoteCrudUseCase
{
    private readonly NotaContext _context;

    public NoteService(NotaContext context)
    {
        _context = context;
    }

    public async Task<Note> FindOne(int id)
    {
        var result = await _context.Notes.FindAsync(id);
        return result ?? throw new EntityNotFoundByIdException<int, Note>(id);
    }

    public async Task<ICollection<Note>> FindMany()
    {
        return await _context.Notes.ToListAsync();
    }

    public async Task<Note> CreateOne(CreateNoteArguments arguments)
    {
        var space = await _context.Spaces.FindAsync(arguments.SpaceId) ??
                    throw new EntityNotFoundByIdException<int, Space>(arguments.SpaceId);

        var note = _context.Notes.Add(new Note
        {
            Title = arguments.Title,
            Content = arguments.Content,
            Space = space
        });

        await _context.SaveChangesAsync();
        return note.Entity;
    }

    public async Task<Note> UpdateOne(UpdateNoteArguments arguments, int id)
    {
        var isChanged = false;
        var note = await FindOne(id);

        var spaceId = arguments.SpaceId;
        var title = arguments.Title;
        var content = arguments.Content;


        if (title != null)
        {
            note.Title = title;
            isChanged = true;
        }

        if (content != null)
        {
            note.Content = content;
            isChanged = true;
        }


        if (spaceId != null)
        {
            note.Space = await _context.Spaces.FindAsync(spaceId) ??
                         throw new EntityNotFoundByIdException<int, Space>((int) spaceId);
            isChanged = true;
        }

        if (!isChanged) return note;

        await _context.SaveChangesAsync();

        return note;
    }

    public async Task DeleteOne(int id)
    {
        var note = await FindOne(id);
        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
    }

    public Task<int> Count()
    {
        return _context.Notes.CountAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Notes.FindAsync(id) is not null;
    }
}