using System;
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
public class NoteCrudUseCaseTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly IWorkspaceCrudUseCase _workspaceCrudUseCase;
    private readonly ISpaceCrudUseCase _spaceCrudUseCase;
    private readonly INoteCrudUseCase _noteCrudUseCase;
    private readonly NotaContext _context;

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    public NoteCrudUseCaseTests(WebApplicationFactory<Program> factory)
    {
        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _workspaceCrudUseCase = scope.ServiceProvider.GetRequiredService<IWorkspaceCrudUseCase>();
        _spaceCrudUseCase = scope.ServiceProvider.GetRequiredService<ISpaceCrudUseCase>();
        _noteCrudUseCase = scope.ServiceProvider.GetRequiredService<INoteCrudUseCase>();
        _context = scope.ServiceProvider.GetRequiredService<NotaContext>();
    }

    [Fact]
    public async Task FindOne_ShouldReturnExistingNote()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var space = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        var reference = await _noteCrudUseCase.CreateOne(new CreateNoteArguments("lesson-1", "example", space.Id));
        var result = await _noteCrudUseCase.FindOne(reference.Id);

        Assert.Equal(reference.Id, result.Id);
        Assert.Equal(reference.Title, result.Title);
        Assert.Equal(reference.Content, result.Content);
        Assert.Equal(reference.Space.Id, result.Space.Id);
    }

    [Fact]
    public async Task FindOne_ThrowsExceptionWhenNoteDoesNotExists()
    {
        await Assert.ThrowsAsync<EntityNotFoundByIdException<int, Note>>(() => _noteCrudUseCase.FindOne(1));
    }


    [Fact]
    public async Task FindMany()
    {
        throw new NotImplementedException();
    }


    [Fact]
    public async Task CreateOne()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var space = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        var note = await _noteCrudUseCase.CreateOne(new CreateNoteArguments("lesson-1", "lorem ipsum", space.Id));

        Assert.Equal(space.Id, note.Space.Id);
        Assert.Equal("lesson-1", note.Title);
        Assert.Equal("lorem ipsum", note.Content);
    }

    [Fact]
    public async Task CreateOne_ThrowsErrorWhenSpaceDoesNotExists()
    {
        var arguments = new CreateNoteArguments("lesson-1", "lorem ipsum", 1);
        await Assert.ThrowsAsync<EntityNotFoundByIdException<int, Space>>(() => _noteCrudUseCase.CreateOne(arguments));
    }


    [Fact]
    public async Task CreateOne_ThrowsErrorWhenTitleIsNotUniqueInsideSpace()
    {
        throw new NotImplementedException();
    }


    [Fact]
    public async Task UpdateOne_ShouldUpdateOnlyTitle()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var space = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        var note = await _noteCrudUseCase.CreateOne(new CreateNoteArguments("lesson-1", "lorem ipsum", space.Id));
        var updatedNote = await _noteCrudUseCase.UpdateOne(new UpdateNoteArguments("lesson-2", null, null), note.Id);

        Assert.Equal(note.Id, updatedNote.Id);
        //TODO: deal with it later Assert.NotEqual(note.Title, updatedNote.Title);
        Assert.Equal("lesson-2", updatedNote.Title);
        Assert.Equal(note.Content, updatedNote.Content);
        Assert.Equal(note.Space.Id, updatedNote.Space.Id);
    }


    [Fact]
    public async Task UpdateOne_ShouldUpdateOnlyContent()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var space = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        var note = await _noteCrudUseCase.CreateOne(new CreateNoteArguments("lesson-1", "lorem ipsum", space.Id));
        var updatedNote = await _noteCrudUseCase.UpdateOne(new UpdateNoteArguments(null, "shriek", null), note.Id);

        Assert.Equal(note.Id, updatedNote.Id);
        Assert.Equal(note.Title, updatedNote.Title);
        //TODO: deal with it later Assert.NotEqual(note.Content, updatedNote.Content);
        Assert.Equal("shriek", updatedNote.Content);
        Assert.Equal(note.Space.Id, updatedNote.Space.Id);
    }


    [Fact]
    public async Task UpdateOne_ShouldUpdateOnlySpace()
    {
        var workspace = await _workspaceCrudUseCase
            .CreateOne(new CreateWorkspaceArguments("School"));

        var space = await _spaceCrudUseCase
            .CreateOne(new CreateSpaceArguments("Math", workspace.Id));

        var anotherSpace = await _spaceCrudUseCase
            .CreateOne(new CreateSpaceArguments("Physics", workspace.Id));

        var note = await _noteCrudUseCase
            .CreateOne(new CreateNoteArguments("lesson-1", "lorem ipsum", space.Id));

        var updatedNote = await _noteCrudUseCase
            .UpdateOne(new UpdateNoteArguments(null, null, anotherSpace.Id), note.Id);


        Assert.Equal(note.Id, updatedNote.Id);
        Assert.Equal(note.Title, updatedNote.Title);
        Assert.Equal(note.Content, updatedNote.Content);
        // TODO: deal with it later Assert.NotEqual(note.Space.Id, updatedNote.Space.Id);
        Assert.Equal(anotherSpace.Id, updatedNote.Space.Id);
    }

    [Fact]
    public async Task UpdateOne_ThrowsErrorWhenTitleIsNotUniqueInsideSpace()
    {
        throw new NotImplementedException();
    }


    [Fact]
    public async Task DeleteOne_DeletesEntity()
    {
        throw new NotEmptyException();
    }


    [Fact]
    public async Task DeleteOne_ThrowsErrorIfNoteDoesNotExists()
    {
        throw new NotEmptyException();
    }


    [Fact]
    public async Task Count()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));

        var mathSpace = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        await _noteCrudUseCase.CreateOne(new CreateNoteArguments("lesson-1", "lorem ipsum", mathSpace.Id));
        await _noteCrudUseCase.CreateOne(new CreateNoteArguments("lesson-2", null, mathSpace.Id));

        var physicsSpace = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Physics", workspace.Id));
        await _noteCrudUseCase.CreateOne(new CreateNoteArguments("lesson-1", "lorem ipsum", physicsSpace.Id));
        await _noteCrudUseCase.CreateOne(new CreateNoteArguments("lesson-2", null, physicsSpace.Id));
    }

    [Fact]
    public async Task Exists_ReturnTrueIfNoteDoesExists()
    {
        var workspace = await _workspaceCrudUseCase.CreateOne(new CreateWorkspaceArguments("School"));
        var space = await _spaceCrudUseCase.CreateOne(new CreateSpaceArguments("Math", workspace.Id));
        var note = await _noteCrudUseCase.CreateOne(new CreateNoteArguments("lesson-1", "lorem ipsum", space.Id));

        var result = await _noteCrudUseCase.Exists(note.Id);

        Assert.True(result);
    }


    [Fact]
    public async Task Exists_ReturnFalseIfNoteDoesNotExists()
    {
        var result = await _noteCrudUseCase.Exists(1);
        Assert.False(result);
    }
}