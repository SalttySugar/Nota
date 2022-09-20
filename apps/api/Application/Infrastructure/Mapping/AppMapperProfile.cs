using Application.Infrastructure.Dto;
using Application.Models;
using AutoMapper;

namespace Application.Infrastructure.Mapping;

public class AppMapperProfile : Profile
{
    public AppMapperProfile()
    {
        CreateMap<Workspace, WorkspaceDto>()
            .ForMember(
                dest => dest.SpacesIds,
                opt =>
                    opt.MapFrom(src => src.Spaces.Map(space => space.Id))
            );
        CreateMap<Space, SpaceDto>()
            .ForMember(
                dest => dest.NotesIds,
                opt =>
                    opt.MapFrom(src => src.Notes.Map(space => space.Id))
            );
        CreateMap<Note, NoteDto>();
    }
}