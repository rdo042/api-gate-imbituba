using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.Deletar
{
    public class DeletarTaskFlowTasksHandler(ITaskFlowTasksRepository taskFlowTasks) 
        : IRequestHandler<DeletarTaskFlowTasksCommand, Result<object?>>
    {
        private readonly ITaskFlowTasksRepository _taskFlowTasksRepository = taskFlowTasks;

        public async Task<Result<object?>> Handle(DeletarTaskFlowTasksCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _taskFlowTasksRepository.RemoveAndShiftAsync(command.FlowId, command.TasksId);

            return result ? Result<object?>.Success(null) : Result<object?>.Failure("Erro ao remover relação e reordenar lista");
        }
    }
}
