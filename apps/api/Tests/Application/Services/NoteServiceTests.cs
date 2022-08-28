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
    // ------------------------------------------------------------ //
    // PROPERTIES
    // ------------------------------------------------------------ //

    private readonly Mock<IRepository<Note>> NoteRepositoryMock = new();
    private readonly Mock<ISpaceService> SpaceServiceMock = new();
    private readonly Mock<Space> SpaceMock = new();
    private readonly INoteService NoteService;

    // ------------------------------------------------------------ //
    // CONSTRUCTOR
    // ------------------------------------------------------------ //

    public NoteServiceTests()
    {
        NoteService = new NoteService(NoteRepositoryMock.Object, SpaceServiceMock.Object);
    }

    // ------------------------------------------------------------ //
    // TESTS
    // ------------------------------------------------------------ //

    [Fact]
    public async Task FindOne()
    {
        _ = NoteRepositoryMock.Setup(m => m.FindOne(1)).ReturnsAsync(Mock.Of<Note?>());
        _ = NoteRepositoryMock.Setup(m => m.FindOne(2)).ReturnsAsync((Note?)null);
        _ = await NoteService.FindOne(1);
        _ = await Assert.ThrowsAsync<NoteNotFoundException>(() => NoteService.FindOne(2));
    }

    [Fact]
    public async Task CreateOne()
    {
        const string NOTE_TITLE = "test note title";
        const string NOTE_CONTENT = "test note content";
        const int NOTE_SPACE_ID = 1;

        _ = SpaceMock.SetupGet(m => m.Id).Returns(NOTE_SPACE_ID);
        _ = SpaceServiceMock.Setup(m => m.FindOne(1)).Returns(SpaceMock.Object.AsTask());
        _ = NoteRepositoryMock.Setup(m => m.Save(It.IsAny<Note>())).Returns<Note>(x => x.AsTask());

        CreateNoteDTO dto =
            new()
            {
                Title = NOTE_TITLE,
                Content = NOTE_CONTENT,
                SpaceId = NOTE_SPACE_ID
            };

        Note result = await NoteService.CreateOne(dto);
        Assert.True(result.Title == dto.Title);
        Assert.True(result.Content == dto.Content);
        Assert.True(result.Space.Id == NOTE_SPACE_ID);
    }

    [Fact]
    public async Task UpdateOne()
    {
        _ = NoteRepositoryMock.Setup(m => m.FindOne(1)).ReturnsAsync(Mock.Of<Note?>());
        _ = NoteRepositoryMock.Setup(m => m.FindOne(2)).ReturnsAsync((Note?)null);

        _ = await NoteService.UpdateOne(1, new UpdateNoteDTO());
        _ = await Assert.ThrowsAsync<NoteNotFoundException>(
            () => NoteService.UpdateOne(2, new UpdateNoteDTO())
        );
    }

    [Fact]
    public async Task DeleteOne()
    {
        _ = NoteRepositoryMock.Setup(m => m.FindOne(1)).ReturnsAsync(Mock.Of<Note?>());
        _ = NoteRepositoryMock.Setup(m => m.FindOne(2)).ReturnsAsync((Note?)null);

        await NoteService.DeleteOne(1);
        _ = await Assert.ThrowsAsync<NoteNotFoundException>(() => NoteService.DeleteOne(2));
    }
}
