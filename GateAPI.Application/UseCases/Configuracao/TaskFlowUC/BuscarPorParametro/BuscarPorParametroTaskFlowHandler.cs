using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowUC.BuscarPorParametro
{
    public class BuscarPorParametroTaskFlowHandler(ITaskFlowRepository taskFlow, ITaskFlowTasksRepository taskFlowTasks) 
        : IRequestHandler<BuscarPorParametroTaskFlowQuery, Result<TaskFlow>>
    {
        private readonly ITaskFlowRepository _taskFlowRepository = taskFlow;
        private readonly ITaskFlowTasksRepository _taskFlowTasksRepository = taskFlowTasks;

        public async Task<Result<TaskFlow>> Handle(BuscarPorParametroTaskFlowQuery command, CancellationToken cancellationToken = default)
        {
            var result = await _taskFlowRepository.GetByIdAsync(command.Id, cancellationToken);

            if(result == null)
                return Result<TaskFlow>.Failure("TaskFlow não encontrado pelo id " + command.Id);

            var tasks = await _taskFlowTasksRepository.GetByFlowIdAsync(result.Id) ?? [];

            if (tasks.Any())
                result.AddTasks(tasks);

            return Result<TaskFlow>.Success(result);
        }
    }
}
