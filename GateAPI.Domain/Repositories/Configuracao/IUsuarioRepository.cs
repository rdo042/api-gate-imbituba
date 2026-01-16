using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Domain.Repositories.Configuracao
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByIdAsync(Guid id);
        Task<Usuario?> GetByEmailAsync(string email);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> AddAsync(Usuario entidade);
        Task UpdateAsync(Usuario entidade);
        Task DeleteAsync(Guid id);
    }
}
