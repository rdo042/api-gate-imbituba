using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Domain.Repositories.Configuracao
{
    public interface ITasksRepository
    {
        Task<IEnumerable<Tasks>> GetAllPorParametroAsync(string nome);
    }
}
