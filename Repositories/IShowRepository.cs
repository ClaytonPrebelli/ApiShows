using ApiShows.Models;

namespace ApiShows.Repositories;

public interface IShowRepository
{
    Task<List<Show>> GetAllAsync();
    Task<Show?> GetByIdAsync(int id);
    Task<Show> CreateAsync(Show show);
    Task<Show?> UpdateAsync(int id, Show show);
    Task<bool> DeleteAsync(int id);
    Task<Show?> TogglePagoAsync(int id);
}
