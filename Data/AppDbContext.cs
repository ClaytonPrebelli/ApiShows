using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ApiShows.Models;

namespace ApiShows.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Show> Shows => Set<Show>();
    public DbSet<Contratante> Contratantes => Set<Contratante>();
    public DbSet<Local> Locais => Set<Local>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var jsonOptions = new JsonSerializerOptions();

        modelBuilder.Entity<Show>(entity =>
        {
            entity.ToTable("Shows");

            entity.Property(e => e.EstilosSolicitados)
                  .HasColumnType("json")
                  .HasConversion(
                      v => JsonSerializer.Serialize(v, jsonOptions),
                      v => JsonSerializer.Deserialize<List<string>>(v, jsonOptions) ?? new())
                  .Metadata.SetValueComparer(
                      new ValueComparer<List<string>>(
                          (a, b) => JsonSerializer.Serialize(a, jsonOptions) == JsonSerializer.Serialize(b, jsonOptions),
                          v => v == null ? 0 : JsonSerializer.Serialize(v, jsonOptions).GetHashCode(),
                          v => JsonSerializer.Deserialize<List<string>>(JsonSerializer.Serialize(v, jsonOptions), jsonOptions) ?? new()));

            entity.HasOne(e => e.Contratante)
                  .WithMany(c => c.Shows)
                  .HasForeignKey(e => e.ContratanteId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Local)
                  .WithMany(l => l.Shows)
                  .HasForeignKey(e => e.LocalId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Contratante>(entity =>
        {
            entity.ToTable("Contratantes");
            entity.HasIndex(e => e.Nome);
        });

        modelBuilder.Entity<Local>(entity =>
        {
            entity.ToTable("Locais");
            entity.HasIndex(e => e.Nome);
        });
    }
}
