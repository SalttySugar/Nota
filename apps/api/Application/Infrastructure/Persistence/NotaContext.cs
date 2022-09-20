using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistence;

public class NotaContext : DbContext
{
    public NotaContext(DbContextOptions<NotaContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<Workspace>()
            .Navigation(e => e.Spaces)
            .AutoInclude();

        modelBuilder
            .Entity<Space>()
            .Navigation(e => e.Workspace)
            .AutoInclude();

        modelBuilder
            .Entity<Space>()
            .Navigation(e => e.Notes)
            .AutoInclude();

        modelBuilder
            .Entity<Note>()
            .Navigation(e => e.Space)
            .AutoInclude();
    }

    public virtual DbSet<Workspace> Workspaces { get; set; }
    public virtual DbSet<Space> Spaces { get; set; }
    public virtual DbSet<Note> Notes { get; set; }
}