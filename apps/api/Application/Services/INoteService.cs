using Application.DTO;
using Application.Models;

namespace Application.Services;

public interface INoteService : ICrudService<Note, CreateNoteDTO, UpdateNoteDTO> { }
