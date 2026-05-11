using Microsoft.EntityFrameworkCore;
using ApiShows.Data;
using ApiShows.Models;

namespace ApiShows.Repositories;

public class ShowRepository : IShowRepository
{
    private readonly AppDbContext _context;

    public ShowRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Show>> GetAllAsync(int? year = null, int? month = null)
    {
        var query = _context.Shows
            .Include(s => s.Contratante)
            .Include(s => s.Local)
            .AsQueryable();

        if (year.HasValue && month.HasValue)
        {
            query = query.Where(s => s.Data.Year == year.Value && s.Data.Month == month.Value);
        }

        return await query
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    public async Task<Show?> GetByIdAsync(int id)
    {
        return await _context.Shows
            .Include(s => s.Contratante)
            .Include(s => s.Local)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Show> CreateAsync(Show show)
    {
        show.Id = 0;
        show.CreatedAt = DateTime.UtcNow;
        _context.Shows.Add(show);
        await _context.SaveChangesAsync();
        return (await _context.Shows
            .Include(s => s.Contratante)
            .Include(s => s.Local)
            .FirstOrDefaultAsync(s => s.Id == show.Id))!;
    }

    public async Task<Show?> UpdateAsync(int id, Show show)
    {
        var existing = await _context.Shows.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        if (existing == null)
            return null;

        show.CreatedAt = existing.CreatedAt;
        _context.Entry(show).State = EntityState.Modified;
        _context.Entry(show).Property(x => x.CreatedAt).IsModified = false;
        await _context.SaveChangesAsync();
        return await _context.Shows
            .Include(s => s.Contratante)
            .Include(s => s.Local)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var show = await _context.Shows.FindAsync(id);
        if (show == null)
            return false;

        _context.Shows.Remove(show);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Show?> TogglePagoAsync(int id)
    {
        var show = await _context.Shows.FindAsync(id);
        if (show == null)
            return null;

        show.Pago = !show.Pago;
        show.DataPagamento = show.Pago ? DateTime.UtcNow : null;
        await _context.SaveChangesAsync();
        return await _context.Shows
            .Include(s => s.Contratante)
            .Include(s => s.Local)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
