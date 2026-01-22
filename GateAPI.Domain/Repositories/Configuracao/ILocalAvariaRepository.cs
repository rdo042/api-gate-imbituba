using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Domain.Repositories.Configuracao
{
    public interface ILocalAvariaRepository : IBaseRepository <LocalAvaria>
    {
        Task<LocalAvaria?> GetByLocalAsync(string local);
        Task<IEnumerable<LocalAvaria>> GetAllAppAsync(CancellationToken cancellationToken = default);
    }
}
