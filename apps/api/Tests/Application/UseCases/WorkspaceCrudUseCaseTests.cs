using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Application.Errors;
using Application.Models;
using Application.Core.UseCases;
using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Sdk;

namespace Tests.Application.UseCases;

[Collection("UseCases")]
public class WorkspaceCrudUseCaseTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly IWorkspaceCrudUseCase _workspaceCrudUseCase;
    private readonly IServiceScope _scope;


    public WorkspaceCrudUseCaseTests(WebApplicationFactory<Program> factory)
    {
        _scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _workspaceCrudUseCase = _scope.ServiceProvider.GetRequiredService<IWorkspaceCrudUseCase>();
    }

    public void Dispose()
    {
        var context = _scope.ServiceProvider.GetRequiredService<NotaContext>();
        context.Database.EnsureDeleted();
        _scope.Dispose();
    }

    [Fact]
    public async Task FindOne_ReturnsExistingWorkspace()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var result = await _workspaceCrudUseCase.FindOne(workspace.Id);

        Assert.Equal(workspace.Id, result.Id);
        Assert.Equal(workspace.Name, result.Name);
    }


    [Fact]
    public async Task FindOne_ThrowsErrorWhenWorkspaceDoesNotExists()
    {
        await Assert.ThrowsAsync<EntityNotFoundByIdException<int, Workspace>>(() => _workspaceCrudUseCase.FindOne(1));
    }


    [Fact]
    public async Task CreateOne()
    {
        var arguments = new CreateWorkspaceArguments("School");
        var workspace = await _workspaceCrudUseCase.CreateOne(arguments);
        var exists = await _workspaceCrudUseCase.Exists(workspace.Id);
        Assert.True(exists);
        Assert.Equal(arguments.Name, workspace.Name);
    }


    [Fact]
    public async Task CreateOne_ThrowsErrorWhenWorkspaceNameIsNotUnique()
    {
        var arguments = new CreateWorkspaceArguments("School");
        await _workspaceCrudUseCase.CreateOne(arguments);
        await _workspaceCrudUseCase.CreateOne(arguments);

        throw new NotImplementedException();
    }


    [Fact]
    public async Task UpdateOne()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var updatedWorkspace =
            await _workspaceCrudUseCase.UpdateOne(new UpdateWorkspaceArguments("University"), workspace.Id);

        Assert.Equal(workspace.Id, updatedWorkspace.Id);
        // TODO: solve this later Assert.NotEqual(workspace.Name, updatedWorkspace.Name);
        Assert.Equal("University", updatedWorkspace.Name);
        Assert.True(updatedWorkspace.UpdatedAt != null);
    }

    [Fact]
    public async Task UpdateOne_ThrowsErrorWhenWorkspaceDoesNotExists()
    {
        await Assert.ThrowsAsync<EntityNotFoundByIdException<int, Workspace>>(() =>
            _workspaceCrudUseCase.UpdateOne(new UpdateWorkspaceArguments(null), 1));
    }

    [Fact]
    public async Task DeleteOne()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        await _workspaceCrudUseCase.DeleteOne(workspace.Id);
        var isWorkspaceExists = await _workspaceCrudUseCase.Exists(workspace.Id);
        Assert.False(isWorkspaceExists);
    }

    [Fact]
    public async Task DeleteOne_ThrowsErrorWhenWorkspaceDoesNotExists()
    {
        await Assert.ThrowsAsync<EntityNotFoundByIdException<int, Workspace>>(() =>
            _workspaceCrudUseCase.DeleteOne(1));
    }


    [Fact]
    public Task DeleteOne_ShouldDeleteSpacesInsideWorkspace()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task Count()
    {
        await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("workspace-1"));
        await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("workspace-2"));
        await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("workspace-3"));

        var result = await _workspaceCrudUseCase.Count();
        Assert.Equal(3, result);
    }


    [Fact]
    public async Task Exists_ReturnsTrueIfWorkspaceDoExists()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var result = await _workspaceCrudUseCase.Exists(workspace.Id);
        Assert.True(result);
    }

    [Fact]
    public async Task Exists_ReturnsFalseIfWorkspaceDoesNotExist()
    {
        var result = await _workspaceCrudUseCase.Exists(1);
        Assert.False(result);
    }

    [Fact]
    public async Task Exits_ReturnsTrueWhenWorkspaceDoesExists()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("Schoo"));
        var isWorkspaceExists = await _workspaceCrudUseCase.Exists(workspace.Id);
        Assert.True(isWorkspaceExists);
    }

    [Fact]
    public async Task Exits_ReturnsFalseWhenWorkspaceDoesNotExists()
    {
        var isWorkspaceExists = await _workspaceCrudUseCase.Exists(1);
        Assert.False(isWorkspaceExists);
    }
}