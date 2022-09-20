using Application.Core.Services;
using Application.Core.UseCases;
using Application.Infrastructure.Mapping;
using Application.Infrastructure.Persistence;
using Infrastructure.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => { options.Filters.Add<HttpExceptionFilter>(); });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// UseCases
builder.Services.AddScoped<IWorkspaceCrudUseCase, WorkspaceService>();
builder.Services.AddScoped<ISpaceCrudUseCase, SpaceService>();
builder.Services.AddScoped<INoteCrudUseCase, NoteService>();

// Persistence
builder.Services.AddDbContext<NotaContext>(
    options => options.UseInMemoryDatabase("Nota")
);

builder.Services.AddAutoMapper(typeof(AppMapperProfile));

// Services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}