using ApiShows.Models;

namespace ApiShows.Repositories;

public interface IContratanteRepository
{
    Task<List<Contratante>> GetAllAsync(string? search = null);
    Task<Contratante?> GetByIdAsync(int id);
    Task<Contratante> CreateAsync(Contratante contratante);
    Task<Contratante?> UpdateAsync(int id, Contratante contratante);
    Task<bool> DeleteAsync(int id);
}
