using Application.DTO;
using Application.Models;
using Persistence.Repository;

namespace Application.Services;

public class NoteService : INoteService
{
    protected IGenericRepository<Note> NoteRepository { get; private set; }
    protected IGenericRepository<Space> SpaceRepository { get; private set; }

    public NoteService(
        IGenericRepository<Note> noteRepository,
        IGenericRepository<Space> spaceRepository
    )
    {
        NoteRepository = noteRepository;
        SpaceRepository = spaceRepository;
    }

    public Task<int> Count()
    {
        throw new NotImplementedException();
    }

    public async Task<Note> CreateOne(CreateNoteDTO createPayload)
    {
        Space space = await SpaceRepository.FindOne(createPayload.SpaceId);
        Note note = new(createPayload.Title, space, DateTime.Now);
        return await NoteRepository.Save(note);
    }

    public Task DeleteOne(int id)
    {
        return NoteRepository.DeleteOne(id);
    }

    public Task<ICollection<Note>> FindMany()
    {
      //TODO: move this to interface 
        return FindMany(new Pageable());
    }

    public Task<ICollection<Note>> FindMany(IPageable pageable)
    {
        throw new NotImplementedException();
    }

    public Task<Note> FindOne(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Note> UpdateOne(int id, UpdateNoteDTO updatePayload)
    {
        throw new NotImplementedException();
    }
}
