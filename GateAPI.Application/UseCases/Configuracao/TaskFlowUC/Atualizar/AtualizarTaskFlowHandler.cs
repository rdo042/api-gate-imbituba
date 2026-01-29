using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowUC.Atualizar
{
    public class AtualizarTaskFlowHandler(ITaskFlowRepository taskFlow) : IRequestHandler<AtualizarTaskFlowCommand, Result<object?>>
    {
        private readonly ITaskFlowRepository _taskFlowRepository = taskFlow;

        public async Task<Result<object?>> Handle(AtualizarTaskFlowCommand command, CancellationToken cancellationToken = default)
        {
            var entidade = await _taskFlowRepository.GetByIdAsync(command.Id, cancellationToken);

            if(entidade == null)
                return Result<object?>.Failure("TaskFlow não encontrado pelo id " + command.Id);

            entidade.UpdateEntity(command.Nome);

            await _taskFlowRepository.UpdateAsync(entidade, cancellationToken);

            return Result<object?>.Success(null);
        }
    }
}
