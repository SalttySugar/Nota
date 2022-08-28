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
    private readonly Mock<IRepository<Note>> NoteRepositoryMock = new();
    private readonly Mock<ISpaceService> SpaceServiceMock = new();
    private readonly Mock<Space> SpaceMock = new();

    [Fact]
    public async Task FindOne()
    {
        NoteRepositoryMock.Setup(m => m.FindOne(1)).ReturnsAsync(Mock.Of<Note?>());
        NoteRepositoryMock.Setup(m => m.FindOne(2)).ReturnsAsync((Note?)null);

        NoteService service = new(NoteRepositoryMock.Object, SpaceServiceMock.Object);

        await service.FindOne(1);
        await Assert.ThrowsAsync<NoteNotFoundException>(() => service.FindOne(2));
    }

    [Fact]
    public async Task CreateOne()
    {
        // constants
        const string NOTE_TITLE = "test note title";
        const string NOTE_CONTENT = "test note content";
        const int NOTE_SPACE_ID = 1;

        // Configure mocks
        SpaceMock.SetupGet(m => m.Id).Returns(NOTE_SPACE_ID);
        SpaceServiceMock.Setup(m => m.FindOne(1)).Returns(SpaceMock.Object.AsTask());
        NoteRepositoryMock.Setup(m => m.Save(It.IsAny<Note>())).Returns<Note>(x => x.AsTask());

        // instance
        NoteService noteService = new(NoteRepositoryMock.Object, SpaceServiceMock.Object);

        // test
        CreateNoteDTO dto =
            new()
            {
                Title = NOTE_TITLE,
                Content = NOTE_CONTENT,
                SpaceId = NOTE_SPACE_ID
            };

        Note result = await noteService.CreateOne(dto);
        Assert.True(result.Title == dto.Title);
        Assert.True(result.Content == dto.Content);
        Assert.True(result.Space.Id == NOTE_SPACE_ID);
    }

    [Fact]
    public async Task UpdateOne()
    {
        NoteRepositoryMock.Setup(m => m.FindOne(1)).ReturnsAsync(Mock.Of<Note?>());
        NoteRepositoryMock.Setup(m => m.FindOne(2)).ReturnsAsync((Note?)null);

        NoteService service = new(NoteRepositoryMock.Object, SpaceServiceMock.Object);

        await service.UpdateOne(1, new UpdateNoteDTO());
        await Assert.ThrowsAsync<NoteNotFoundException>(
            () => service.UpdateOne(2, new UpdateNoteDTO())
        );
    }

    [Fact]
    public async Task DeleteOne()
    {
        NoteRepositoryMock.Setup(m => m.FindOne(1)).ReturnsAsync(Mock.Of<Note?>());
        NoteRepositoryMock.Setup(m => m.FindOne(2)).ReturnsAsync((Note?)null);

        NoteService service = new(NoteRepositoryMock.Object, SpaceServiceMock.Object);

        await service.DeleteOne(1);
        await Assert.ThrowsAsync<NoteNotFoundException>(() => service.DeleteOne(2));
    }
}
