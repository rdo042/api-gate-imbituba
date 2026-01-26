using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Domain.Repositories.Configuracao
{
    public interface IIdiomaRepository : IBaseRepository<Idioma>
    {
        Task<IEnumerable<Idioma>> GetAllAtivosAsync(CancellationToken cancellationToken = default);
        Task<Idioma?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default);
        Task<Idioma?> GetPadraoAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Idioma>> GetByCanalAsync(int canal, CancellationToken cancellationToken = default);
    }
}
