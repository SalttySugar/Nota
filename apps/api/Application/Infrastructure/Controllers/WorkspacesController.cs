using Application.Core.UseCases;
using Application.Infrastructure.Dto;
using AutoMapper;
using Infrastructure.Constants;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;

namespace Application.Infrastructure.Controllers;

[ApiController]
[Route($"{Api.V1}/[controller]")]
public class WorkspacesController
{
    private readonly IWorkspaceCrudUseCase _workspaceCrudUseCase;
    private readonly IMapper _mapper;

    public WorkspacesController(IWorkspaceCrudUseCase workspaceCrudUseCase, IMapper mapper)
    {
        _workspaceCrudUseCase = workspaceCrudUseCase;
        _mapper = mapper;
    }


    [HttpGet("{id:int}")]
    public Task<WorkspaceDto> FindOne(int id) =>
        _workspaceCrudUseCase
            .FindOne(id)
            .Map(workspace => _mapper.Map<WorkspaceDto>(workspace));

    [HttpGet]
    public Task<ICollection<WorkspaceDto>> FindMany() =>
        _workspaceCrudUseCase
            .FindMany()
            .Map(workspace => _mapper.Map<ICollection<WorkspaceDto>>(workspace));


    [HttpPost]
    public Task<WorkspaceDto> CreateOne(CreateWorkspaceArguments arguments) =>
        _workspaceCrudUseCase
            .CreateOne(arguments)
            .Map(workspace => _mapper.Map<WorkspaceDto>(workspace));


    [HttpPut("{id:int}")]
    public Task<WorkspaceDto> UpdateOne(UpdateWorkspaceArguments arguments, int id) =>
        _workspaceCrudUseCase
            .UpdateOne(arguments, id)
            .Map(workspace => _mapper.Map<WorkspaceDto>(workspace));


    [HttpDelete("{id:int}")]
    public Task DeleteOne(int id) => _workspaceCrudUseCase.DeleteOne(id);


    [HttpGet("Count")]
    public Task<int> Count(int id) => _workspaceCrudUseCase.Count();
}