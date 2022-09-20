using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Core.UseCases;
using Application.Errors;
using Application.Infrastructure.Persistence;
using Application.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Sdk;

namespace Tests.Application.UseCases;

[Collection("UseCases")]
public class SpaceCrudUseCaseTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly ISpaceCrudUseCase _spaceCrudUseCase;
    private readonly IWorkspaceCrudUseCase _workspaceCrudUseCase;
    private readonly NotaContext _context;

    public SpaceCrudUseCaseTests(WebApplicationFactory<Program> factory)
    {
        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _spaceCrudUseCase = scope.ServiceProvider.GetRequiredService<ISpaceCrudUseCase>();
        _workspaceCrudUseCase = scope.ServiceProvider.GetRequiredService<IWorkspaceCrudUseCase>();
        _context = scope.ServiceProvider.GetRequiredService<NotaContext>();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task FindOne()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var space = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        var result = await _spaceCrudUseCase.FindOne(space.Id);
        Assert.Equal(space, result);
    }


    [Fact]
    public async Task FindOne_ThrowsErrorWhenSpaceDoesNotExists()
    {
        await Assert.ThrowsAsync<EntityNotFoundByIdException<int, Space>>(
            () => _spaceCrudUseCase.FindOne(1)
        );
    }


    [Fact]
    public async Task FindMany_ReturnsAllRecords()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Physics", workspace.Id));
        await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("History", workspace.Id));
        await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("English", workspace.Id));
        await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Programming", workspace.Id));

        var result = await _spaceCrudUseCase.FindMany();
        Assert.Contains(result, space => space.Name == "Math");
        Assert.Contains(result, space => space.Name == "Physics");
        Assert.Contains(result, space => space.Name == "History");
        Assert.Contains(result, space => space.Name == "English");
        Assert.Contains(result, space => space.Name == "Programming");
    }


    [Fact]
    public async Task CreateOne()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var space = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));

        Assert.Equal(workspace, space.Workspace);
        Assert.Equal("Math", space.Name);
    }


    [Fact]
    public async Task CreateOne_ThrowsErrorWhenWorkspaceDoesNotExists()
    {
        await Assert.ThrowsAsync<EntityNotFoundByIdException<int, Workspace>>(
            () => _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", 10))
        );
    }


    [Fact]
    public async Task CreateOne_ThrowsErrorWhenSpaceNameIsNotUniqueInWorkspace()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var arguments = new CreateSpaceArguments("Math", workspace.Id);
        await _spaceCrudUseCase.CreateOne(arguments);
        throw new NotImplementedException();
    }


    [Fact]
    public async Task UpdateOne_ShouldUpdateOnlyName()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var space = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        var updatedSpace = await _spaceCrudUseCase.UpdateOne(new UpdateSpaceArguments("Physic", null), space.Id);

        Assert.Equal("Physic", updatedSpace.Name);
        Assert.Equal(workspace, updatedSpace.Workspace);
    }


    [Fact]
    public async Task UpdateOne_ShouldUpdateOnlyWorkspace()
    {
        var schoolWorkspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var universityWorkspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("University"));
        var space = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", schoolWorkspace.Id));
        var updatedSpace =
            await _spaceCrudUseCase.UpdateOne(new UpdateSpaceArguments(null, universityWorkspace.Id), space.Id);

        Assert.Equal("Math", updatedSpace.Name);
        Assert.Equal(universityWorkspace, updatedSpace.Workspace);
    }


    [Fact]
    public Task UpdateOne_ThrowsErrorWhenSpaceNameIsNotUniqueInSpace()
    {
        throw new NotImplementedException();
    }


    [Fact]
    public Task UpdateOne_ThrowsExceptionWhenSpaceNameIsNotUniqueInWorkspace()
    {
        throw new NotImplementedException();
    }


    [Fact]
    public async Task UpdateOne_ThrowsErrorWhenWorkspaceDoesNotExists()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var space = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        await Assert.ThrowsAsync<EntityNotFoundByIdException<int, Workspace>>(
            () => _spaceCrudUseCase.UpdateOne(new UpdateSpaceArguments(null, 10), space.Id)
        );
    }


    [Fact]
    public async Task DeleteOne_DeletesSpace()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var space = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        var isSpaceExists = await _spaceCrudUseCase.Exists(space.Id);

        Assert.False(isSpaceExists);
    }


    [Fact]
    public async Task DeleteOne_ThrowsErrorWhenSpaceDoesNotExists()
    {
        await Assert.ThrowsAsync<EntityNotFoundByIdException<int, Space>>(
            () => _spaceCrudUseCase.DeleteOne(1)
        );
    }


    [Fact]
    public async Task Count_ReturnsTotalNumberOfSpaces()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Physics", workspace.Id));
        await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("History", workspace.Id));
        await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("English", workspace.Id));
        await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Programming", workspace.Id));


        var count = await _spaceCrudUseCase.Count();
        Assert.Equal(5, count);
    }


    [Fact]
    public async Task Exists_ReturnsTrueIfSpaceDoesExists()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var space = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        var isSpaceExists = await _spaceCrudUseCase.Exists(space.Id);
        Assert.True(isSpaceExists);
    }


    [Fact]
    public async Task Exists_ReturnsFalseIfSpaceDoesNotExists()
    {
        var isSpaceExists = await _spaceCrudUseCase.Exists(10);
        Assert.False(isSpaceExists);
    }
}