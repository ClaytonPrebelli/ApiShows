using Microsoft.EntityFrameworkCore;
using ApiShows.Data;
using ApiShows.Models;

namespace ApiShows.Repositories;

public class ContratanteRepository : IContratanteRepository
{
    private readonly AppDbContext _context;

    public ContratanteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Contratante>> GetAllAsync(string? search = null)
    {
        var query = _context.Contratantes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => c.Nome.Contains(search));

        return await query
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<Contratante?> GetByIdAsync(int id)
    {
        return await _context.Contratantes.FindAsync(id);
    }

    public async Task<Contratante> CreateAsync(Contratante contratante)
    {
        contratante.Id = 0;
        contratante.CreatedAt = DateTime.UtcNow;
        _context.Contratantes.Add(contratante);
        await _context.SaveChangesAsync();
        return contratante;
    }

    public async Task<Contratante?> UpdateAsync(int id, Contratante contratante)
    {
        var existing = await _context.Contratantes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        if (existing == null)
            return null;

        contratante.CreatedAt = existing.CreatedAt;
        _context.Entry(contratante).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return contratante;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var contratante = await _context.Contratantes.FindAsync(id);
        if (contratante == null)
            return false;

        var hasShows = await _context.Shows.AnyAsync(s => s.ContratanteId == id);
        if (hasShows)
            return false;

        _context.Contratantes.Remove(contratante);
        await _context.SaveChangesAsync();
        return true;
    }
}
