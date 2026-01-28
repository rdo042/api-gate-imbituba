using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Domain.Repositories.Configuracao
{
    public interface ITaskFlowTasksRepository
    {
        Task<TaskFlowTasks> AddAsync(TaskFlowTasks relation);
        Task<IEnumerable<TaskFlowTasks>> GetByFlowIdAsync(Guid flowId);
        Task<TaskFlowTasks?> GetSpecificAsync(Guid flowId, Guid taskId);
        Task<TaskFlowTasks?> GetByOrderAsync(Guid flowId, int ordem);
        Task<int> GetMaxOrdemAsync(Guid flowId);
        Task RemoveAndShiftAsync(Guid flowId, Guid taskId);
        Task<bool> Remove(Guid id);
        Task<bool> RemoveByFlow(Guid flowId);
    }
}
