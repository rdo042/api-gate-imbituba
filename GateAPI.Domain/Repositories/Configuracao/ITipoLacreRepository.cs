using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Domain.Repositories.Configuracao
{
    public interface ITipoLacreRepository
    {
        Task<TipoLacre?> GetByIdAsync(Guid id);
        Task<IEnumerable<TipoLacre>> GetAllAsync();
        Task<TipoLacre> AddAsync(TipoLacre entidade);
        Task UpdateAsync(TipoLacre entidade);
        Task DeleteAsync(Guid id);
    }
}
