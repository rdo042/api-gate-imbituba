using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.Criar
{
    public class CriarTaskFlowTasksHandler(
        ITaskFlowTasksRepository taskFlowTasks,
        ITaskFlowRepository taskFlow,
        ITasksRepository tasks) 
        : IRequestHandler<CriarTaskFlowTasksCommand, Result<int>>
    {
        private readonly ITaskFlowTasksRepository _taskFlowTasksRepository = taskFlowTasks;
        private readonly ITaskFlowRepository _taskFlowRepository = taskFlow;
        private readonly ITasksRepository _tasksRepository = tasks;

        public async Task<Result<int>> Handle(CriarTaskFlowTasksCommand command, CancellationToken cancellationToken = default)
        {
            var taskFlow = await _taskFlowRepository.GetByIdAsync(command.FlowId, cancellationToken);
            if (taskFlow == null)
                return Result<int>.Failure("Fluxo de Tarefas não encontrado.");

            var tasks = await _tasksRepository.GetByIdAsync(command.TasksId, cancellationToken);
            if (tasks == null)
                return Result<int>.Failure("Tarefa não encontrada.");

            var ordem = await _taskFlowTasksRepository.GetMaxOrdemAsync(command.FlowId);
            var proximaOrdem = ordem + 1;

            var obj = new TaskFlowTasks(taskFlow, tasks, proximaOrdem, Domain.Enums.StatusEnum.ATIVO);
            _ = await _taskFlowTasksRepository.AddAsync(obj);

            return Result<int>.Success(proximaOrdem);
        }
    }
}
