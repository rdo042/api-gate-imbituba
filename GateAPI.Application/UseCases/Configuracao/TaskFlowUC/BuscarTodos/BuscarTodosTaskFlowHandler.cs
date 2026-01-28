using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowUC.BuscarTodos
{
    public class BuscarTodosTaskFlowHandler(ITaskFlowRepository taskFlow) : IRequestHandler<BuscarTodosTaskFlowQuery, Result<IEnumerable<TaskFlow>>>
    {
        private readonly ITaskFlowRepository _taskFlowRepository = taskFlow;

        public async Task<Result<IEnumerable<TaskFlow>>> Handle(BuscarTodosTaskFlowQuery command, CancellationToken cancellationToken = default)
        {
            var result = await _taskFlowRepository.GetAllAsync(cancellationToken);

            return Result<IEnumerable<TaskFlow>>.Success(result);
        }
    }
}
