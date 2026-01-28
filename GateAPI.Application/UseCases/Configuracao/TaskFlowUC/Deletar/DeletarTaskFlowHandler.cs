using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowUC.Deletar
{
    public class DeletarTaskFlowHandler(ITaskFlowRepository taskFlow) : IRequestHandler<DeletarTaskFlowCommand, Result<object?>>
    {
        private readonly ITaskFlowRepository _taskFlowRepository = taskFlow;

        public async Task<Result<object?>> Handle(DeletarTaskFlowCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _taskFlowRepository.DeleteAsync(command.Id, cancellationToken);

            if (!result)
                return Result<object?>.Failure("TaskFlow não encontrado pelo id " + command.Id);

            return Result<object?>.Success(null);
        }
    }
}
