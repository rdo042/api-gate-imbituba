using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.AlterarStatus
{
    public class AlterarStatusTaskFlowTasksHandler(ITaskFlowTasksRepository taskFlowTasks)
    {
        private readonly ITaskFlowTasksRepository _taskFlowTasksRepository = taskFlowTasks;

        public async Task<Result<object?>> Handle(AlterarStatusTaskFlowTasksCommand command, CancellationToken cancellationToken = default)
        {
            var entidade = await _taskFlowTasksRepository.GetSpecificAsync(command.FlowId, command.TasksId);
            if (entidade == null)
                return Result<object?>.Failure("Relação não encontrada");

            entidade.AlterarStatus(command.Status);

            await _taskFlowTasksRepository.UpdateRange([entidade]);

            return Result<object?>.Success(null);
        }

    }
}
