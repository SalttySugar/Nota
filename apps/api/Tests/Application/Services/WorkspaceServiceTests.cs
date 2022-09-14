using Xunit;
using Moq;
using System.Threading.Tasks;
using Persistence.Repository;
using Application.Models;
using Application.Services;
using Application.Errors;
using LanguageExt;
using Application.DTO;

namespace Tests.Application.Services;

public class WorkspaceServiceTests
{
    // =================================================================================
    // PROPERTIES
    // =================================================================================

    private readonly Mock<IRepository<Workspace>> workspaceRepositoryMock = new();

    private readonly IWorkspaceService workspaceService;

    // =================================================================================
    // CONSTRUCTOR
    // =================================================================================

    public WorkspaceServiceTests()
    {
        workspaceService = new WorkspaceService(workspaceRepositoryMock.Object);
    }

    // =================================================================================
    // TESTS
    // =================================================================================

    // ------------------------------------------------------------ //
    // FindOne
    // ------------------------------------------------------------ //

    [Fact]
    public async Task FindOne_returns_workspace()
    {
        Workspace expected = Mock.Of<Workspace>();
        _ = workspaceRepositoryMock.Setup(m => m.FindOne(1)).ReturnsAsync(expected);
        Workspace result = await workspaceService.FindOne(1);
    }

    [Fact]
    public async Task FindOne_throws_error_when_workspace_not_found()
    {
        _ = workspaceRepositoryMock.Setup(m => m.FindOne(1)).ReturnsAsync((Workspace?)null);
        _ = await Assert.ThrowsAsync<WorkspaceNotFoundById>(() => workspaceService.FindOne(1));
    }

    // ------------------------------------------------------------ //
    // CreateOne
    // ------------------------------------------------------------ //

    [Fact]
    public async Task CreateOne_creates_and_returns_persisted_workspace()
    {
        _ = workspaceRepositoryMock
            .Setup(m => m.Save(It.IsAny<Workspace>()))
            .Returns<Workspace>(x => x.AsTask());
        EditWorkspaceDTO dto = new() { Name = "example" };
        Workspace result = await workspaceService.CreateOne(dto);
        Assert.Equal(dto.Name, result.Name);
    }

    // ------------------------------------------------------------ //
    // UpdateOne
    // ------------------------------------------------------------ //

    [Fact]
    public async Task UpdateOne_updates_existing_workspace()
    {
        _ = workspaceRepositoryMock.Setup(m => m.FindOne(1)).ReturnsAsync(Mock.Of<Workspace>());
        _ = workspaceRepositoryMock
            .Setup(m => m.Save(It.IsAny<Workspace>()))
            .Returns<Workspace>(a => a.AsTask());

        Workspace stored = new() { Id = 1, Name = "test-workspace" };
        EditWorkspaceDTO dto = new() { Name = "", };
        Workspace result = await workspaceService.UpdateOne(1, dto);

        Assert.Equal(dto.Name, result.Name);
        workspaceRepositoryMock.Verify(m => m.Save(It.IsAny<Workspace>()), Times.Once());
    }

    [Fact]
    public async Task UpdateOne_throws_error_when_workspace_does_not_exists()
    {
        _ = workspaceRepositoryMock.Setup(m => m.FindOne(1)).ReturnsAsync((Workspace?)null);
        _ = await Assert.ThrowsAsync<WorkspaceNotFoundById>(
            () => workspaceService.UpdateOne(id: 1, Mock.Of<EditWorkspaceDTO>())
        );
    }

    // ------------------------------------------------------------ //
    // DeleteOne
    // ------------------------------------------------------------ //

    [Fact]
    public async Task DeleteOne_deletes_workspace()
    {
        _ = workspaceRepositoryMock.Setup(m => m.DeleteOne(1));
        _ = workspaceRepositoryMock.Setup(m => m.FindOne(1)).ReturnsAsync(Mock.Of<Workspace>());

        await workspaceService.DeleteOne(1);

        workspaceRepositoryMock.Verify(m => m.DeleteOne(1), Times.Once());
    }

    [Fact]
    public async Task DeleteOne_throws_error_when_workspace_does_not_exists()
    {
        _ = workspaceRepositoryMock.Setup(m => m.FindOne(1)).ReturnsAsync((Workspace?)null);
        _ = await Assert.ThrowsAsync<WorkspaceNotFoundById>(() => workspaceService.DeleteOne(1));
    }

    // ------------------------------------------------------------ //
    // Count
    // ------------------------------------------------------------ //

    [Fact]
    public async Task Count_should_return_total_records()
    {
        _ = workspaceRepositoryMock.Setup(m => m.Count()).ReturnsAsync(10);
        int result = await workspaceService.Count();
        Assert.Equal(10, result);
    }
}
