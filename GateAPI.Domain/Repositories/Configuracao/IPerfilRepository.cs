using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Domain.Repositories.Configuracao
{
    public interface IPerfilRepository
    {
        Task<Perfil?> GetByIdAsync(Guid id);
        Task<IEnumerable<Perfil>> GetAllAsync();
        Task<Perfil> AddAsync(Perfil entidade);
        Task UpdateAsync(Perfil entidade);
        Task DeleteAsync(Guid id);
    }
}
