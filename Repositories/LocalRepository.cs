using Microsoft.EntityFrameworkCore;
using ApiShows.Data;
using ApiShows.Models;

namespace ApiShows.Repositories;

public class LocalRepository : ILocalRepository
{
    private readonly AppDbContext _context;

    public LocalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Local>> GetAllAsync(string? search = null)
    {
        var query = _context.Locais.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(l => l.Nome.Contains(search));

        return await query
            .OrderBy(l => l.Nome)
            .ToListAsync();
    }

    public async Task<Local?> GetByIdAsync(int id)
    {
        return await _context.Locais.FindAsync(id);
    }

    public async Task<Local> CreateAsync(Local local)
    {
        local.Id = 0;
        local.CreatedAt = DateTime.UtcNow;
        _context.Locais.Add(local);
        await _context.SaveChangesAsync();
        return local;
    }

    public async Task<Local?> UpdateAsync(int id, Local local)
    {
        var existing = await _context.Locais.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
        if (existing == null)
            return null;

        local.CreatedAt = existing.CreatedAt;
        _context.Entry(local).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return local;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var local = await _context.Locais.FindAsync(id);
        if (local == null)
            return false;

        var hasShows = await _context.Shows.AnyAsync(s => s.LocalId == id);
        if (hasShows)
            return false;

        _context.Locais.Remove(local);
        await _context.SaveChangesAsync();
        return true;
    }
}
