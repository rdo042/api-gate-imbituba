using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Domain.Repositories.Configuracao
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByIdAsync(Guid id);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<(IEnumerable<Usuario>, int)> GetAllPaginatedAsync(int page, int pageSize, string? sortColumn, string sortDirection, string? nome);
        Task<Usuario> AddAsync(Usuario entidade);
        Task UpdateAsync(Usuario entidade);
        Task DeleteAsync(Guid id);

        Task<Usuario?> GetByEmailAsync(string email);
    }
}
