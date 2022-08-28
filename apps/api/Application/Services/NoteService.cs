using Application.DTO;
using Application.Errors;
using Application.Models;
using Persistence.Repository;

namespace Application.Services;

public class NoteService : INoteService
{
    // ------------------------------------------------------------ //
    // PROPERTIES
    // ------------------------------------------------------------ //

    protected IRepository<Note> NoteRepository { get; private set; }
    protected ISpaceService SpaceService { get; private set; }

    // ------------------------------------------------------------ //
    // CONSTRUCTORS
    // ------------------------------------------------------------ //

    public NoteService(IRepository<Note> noteRepository, ISpaceService spaceService)
    {
        NoteRepository = noteRepository;
        SpaceService = spaceService;
    }

    // ------------------------------------------------------------ //
    // METHODS
    // ------------------------------------------------------------ //

    public async Task<Note> FindOne(int id)
    {
        Note? note = await NoteRepository.FindOne(id) ?? throw new NoteNotFoundException(id);
        return note;
    }

    public Task<ICollection<Note>> FindMany()
    {
        return NoteRepository.FindMany(null, null, new Pageable());
    }

    public Task<ICollection<Note>> FindMany(IPageable pageable)
    {
        return NoteRepository.FindMany(null, null, pageable);
    }

    public async Task<Note> CreateOne(CreateNoteDTO createPayload)
    {
        Space space = await SpaceService.FindOne(createPayload.SpaceId);

        Note note =
            new()
            {
                Title = createPayload.Title,
                Content = createPayload.Content,
                Space = space,
            };

        return await NoteRepository.Save(note);
    }

    public async Task<Note> UpdateOne(int id, UpdateNoteDTO updatePayload)
    {
        Note note = await FindOne(id);

        if (updatePayload.Title != null)
        {
            note.Title = updatePayload.Title;
        }

        if (updatePayload.Content != null)
        {
            note.Content = updatePayload.Content;
        }

        if (updatePayload.SpaceId != null)
        {
            note.Space = await SpaceService.FindOne((int)updatePayload.SpaceId);
        }

        return await NoteRepository.Save(note);
    }

    public async Task DeleteOne(int id)
    {
        _ = await FindOne(id);
        await NoteRepository.DeleteOne(id);
    }

    public Task<int> Count()
    {
        return NoteRepository.Count();
    }
}
