using ApiShows.Models;

namespace ApiShows.Repositories;

public interface ILocalRepository
{
    Task<List<Local>> GetAllAsync();
    Task<Local?> GetByIdAsync(int id);
    Task<Local> CreateAsync(Local local);
    Task<Local?> UpdateAsync(int id, Local local);
    Task<bool> DeleteAsync(int id);
}
