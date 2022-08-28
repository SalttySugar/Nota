using Xunit;
using Moq;
using Application.Models;
using Application.DTO;
using Application.Services;
using Persistence.Repository;
using LanguageExt;
using System.Threading.Tasks;
using Application.Errors;

namespace Tests.Application.Services;

public class NoteServiceTests
{
    // =================================================================================
    // PROPERTIES
    // =================================================================================

    private readonly Mock<IRepository<Note>> NoteRepositoryMock = new();
    private readonly Mock<ISpaceService> SpaceServiceMock = new();
    private readonly Mock<Space> SpaceMock = new();
    private readonly INoteService NoteService;

    // =================================================================================
    // CONSTRUCTORS
    // =================================================================================

    public NoteServiceTests()
    {
        NoteService = new NoteService(NoteRepositoryMock.Object, SpaceServiceMock.Object);
    }

    // =================================================================================
    // TESTS
    // =================================================================================

    // ------------------------------------------------------------ //
    // FindOne
    // ------------------------------------------------------------ //

    [Fact]
    public async Task FindOne_returns_note()
    {
        Note note = Mock.Of<Note>();
        _ = NoteRepositoryMock.Setup(m => m.FindOne(2)).ReturnsAsync(note);
        Note result = await NoteService.FindOne(2);
        Assert.Equal(note, result);
    }

    [Fact]
    public async Task FindOne_throws_error_when_note_not_found()
    {
        _ = NoteRepositoryMock.Setup(m => m.FindOne(2)).ReturnsAsync((Note?)null);
        _ = await Assert.ThrowsAsync<NoteNotFoundException>(() => NoteService.FindOne(2));
    }

    // ------------------------------------------------------------ //
    // CreateOne
    // ------------------------------------------------------------ //

    [Fact]
    public async Task CreateOne()
    {
        const string NOTE_TITLE = "test title";
        const string NOTE_CONTENT = "test content";
        const int NOTE_SPACE_ID = 3;
        Space space = Mock.Of<Space>();

        CreateNoteDTO createNoteDto =
            new()
            {
                Title = NOTE_TITLE,
                Content = NOTE_CONTENT,
                SpaceId = NOTE_SPACE_ID
            };

        _ = SpaceServiceMock.Setup(m => m.FindOne(NOTE_SPACE_ID)).ReturnsAsync(space);
        _ = NoteRepositoryMock.Setup(m => m.Save(It.IsAny<Note>())).Returns<Note>(x => x.AsTask());

        Note result = await NoteService.CreateOne(createNoteDto);

        Assert.Equal(NOTE_TITLE, result.Title);
        Assert.Equal(NOTE_CONTENT, result.Content);
        Assert.Equal(space, result.Space);
    }

    // ------------------------------------------------------------ //
    // UpdateOne
    // ------------------------------------------------------------ //

    [Fact]
    public async Task UpdateOne_updates_title_and_returns_updated_note()
    {
        const string NOTE_TITLE = "new-note-title";

        Mock<Note> mockNote = new();


        _ = NoteRepositoryMock.Setup(m => m.FindOne(2)).ReturnsAsync(mockNote.Object);
        _ = NoteRepositoryMock.Setup(m => m.Save(It.IsAny<Note>())).Returns<Note>(x => x.AsTask());

        UpdateNoteDTO updateNoteDTO = new() { Title = NOTE_TITLE };

        Note result = await NoteService.UpdateOne(2, updateNoteDTO);

        Assert.Equal(mockNote.Object, result);

        mockNote.VerifySet(m => m.Title = NOTE_TITLE, Times.Once());
        mockNote.VerifySet(m => m.Content = It.IsAny<string>(), Times.Never());
        mockNote.VerifySet(m => m.Space = It.IsAny<Space>(), Times.Never());
    }

    [Fact]
    public async Task UpdateOne_throws_error_when_note_not_found()
    {
        _ = NoteRepositoryMock.Setup(m => m.FindOne(2)).ReturnsAsync((Note?)null);
        _ = await Assert.ThrowsAsync<NoteNotFoundException>(() => NoteService.FindOne(2));
    }

    // ------------------------------------------------------------ //
    // DeleteOne
    // ------------------------------------------------------------ //

    [Fact]
    public async Task DeleteOne_deletes_existing_note()
    {
        _ = NoteRepositoryMock.Setup(m => m.FindOne(2)).ReturnsAsync(Mock.Of<Note>());
        await NoteService.DeleteOne(2);
        NoteRepositoryMock.Verify(m => m.DeleteOne(2), Times.Once());
    }

    [Fact]
    public async Task DeleteOne_throws_error_when_note_not_found()
    {
        _ = NoteRepositoryMock.Setup(m => m.FindOne(2)).ReturnsAsync((Note?)null);
        _ = await Assert.ThrowsAsync<NoteNotFoundException>(() => NoteService.DeleteOne(2));
    }

    // ------------------------------------------------------------ //
    // Count
    // ------------------------------------------------------------ //

    [Fact]
    public async Task Count()
    {
        _ = NoteRepositoryMock.Setup(m => m.Count()).ReturnsAsync(15);
        int result = await NoteService.Count();
        Assert.Equal(15, result);
    }
}
