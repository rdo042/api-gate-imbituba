using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowUC.Criar
{
    public class CriarTaskFlowHandler(ITaskFlowRepository taskFlow) : IRequestHandler<CriarTaskFlowCommand, Result<TaskFlow>>
    {
        private readonly ITaskFlowRepository _taskFlowRepository = taskFlow;

        public async Task<Result<TaskFlow>> Handle(CriarTaskFlowCommand command, CancellationToken cancellationToken = default)
        {
            var obj = new TaskFlow(command.Nome);
            var result = await _taskFlowRepository.AddAsync(obj, cancellationToken);

            return Result<TaskFlow>.Success(result);
        }
    }
}
