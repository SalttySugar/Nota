using Application.Core.UseCases;
using Application.Infrastructure.Dto;
using AutoMapper;
using Infrastructure.Constants;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;

namespace Application.Infrastructure.Controllers;

[ApiController]
[Route($"{Api.V1}/[controller]")]
public class NotesController
{
    private readonly INoteCrudUseCase _noteCrudUseCase;
    private readonly IMapper _mapper;

    public NotesController(INoteCrudUseCase noteCrudUseCase, IMapper mapper)
    {
        _noteCrudUseCase = noteCrudUseCase;
        _mapper = mapper;
    }


    [HttpGet("{id:int}")]
    public Task<NoteDto> FindOne(int id) => _noteCrudUseCase
        .FindOne(id)
        .Map(note => _mapper.Map<NoteDto>(note));


    [HttpGet]
    public Task<ICollection<NoteDto>> FindMany() => _noteCrudUseCase
        .FindMany()
        .Map(notes => _mapper.Map<ICollection<NoteDto>>(notes));


    [HttpPost]
    public Task<NoteDto> CreateOne(CreateNoteArguments arguments) => _noteCrudUseCase
        .CreateOne(arguments)
        .Map(note => _mapper.Map<NoteDto>(note));


    [HttpPatch("{id:int}")]
    public Task<NoteDto> UpdateOne(UpdateNoteArguments arguments, int id) => _noteCrudUseCase
        .UpdateOne(arguments, id)
        .Map(note => _mapper.Map<NoteDto>(note));


    [HttpDelete("{id:int}")]
    public Task DeleteOne(int id) => _noteCrudUseCase.DeleteOne(id);


    [HttpGet("Count")]
    public Task<int> Count() => _noteCrudUseCase.Count();
}