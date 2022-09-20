using Application.Core.UseCases;
using Application.Infrastructure.Dto;
using AutoMapper;
using Infrastructure.Constants;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;

namespace Application.Infrastructure.Controllers;

[ApiController]
[Route($"${Api.V1}/[controller]")]
public class SpacesController
{
    private readonly ISpaceCrudUseCase _spaceCrudUseCase;

    private readonly IMapper _mapper;

    public SpacesController(ISpaceCrudUseCase spaceCrudUseCase, IMapper mapper)
    {
        _spaceCrudUseCase = spaceCrudUseCase;
        _mapper = mapper;
    }


    [HttpGet("{id:int}")]
    public Task<SpaceDto> FindOne(int id) =>
        _spaceCrudUseCase
            .FindOne(id)
            .Map(space => _mapper.Map<SpaceDto>(space));


    [HttpGet]
    public Task<ICollection<SpaceDto>> FindMany(int id) =>
        _spaceCrudUseCase
            .FindMany()
            .Map(spaces => _mapper.Map<ICollection<SpaceDto>>(spaces));


    [HttpPost]
    public Task<SpaceDto> CreateOne(CreateSpaceArguments arguments) =>
        _spaceCrudUseCase
            .CreateOne(arguments)
            .Map(space => _mapper.Map<SpaceDto>(space));


    [HttpPut("{id:int}")]
    public Task<SpaceDto> UpdateOne(UpdateSpaceArguments arguments, int id) =>
        _spaceCrudUseCase
            .UpdateOne(arguments, id)
            .Map(space => _mapper.Map<SpaceDto>(space));


    [HttpDelete("{id:int}")]
    public Task DeleteOne(int id) => _spaceCrudUseCase.DeleteOne(id);


    [HttpGet("Count")]
    public Task<int> Count() => _spaceCrudUseCase.Count();
}