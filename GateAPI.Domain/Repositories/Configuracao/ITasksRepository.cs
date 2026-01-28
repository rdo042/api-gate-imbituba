using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Domain.Repositories.Configuracao
{
    public interface ITasksRepository
    {
        Task<Tasks?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Tasks>> GetAllPorParametroAsync(string nome);
    }
}
