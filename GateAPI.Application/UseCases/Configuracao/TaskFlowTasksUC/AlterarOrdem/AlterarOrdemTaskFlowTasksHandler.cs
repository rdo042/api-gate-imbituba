using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.AlterarOrdem
{
    public class AlterarOrdemTaskFlowTasksHandler(ITaskFlowTasksRepository taskFlowTasks)
    {
        private readonly ITaskFlowTasksRepository _taskFlowTasksRepository = taskFlowTasks;


        public async Task<Result<object?>> Handle(AlterarOrdemTaskFlowTasksCommand command, CancellationToken cancellationToken = default)
        {
            var relacaoAlvo = await _taskFlowTasksRepository.GetSpecificAsync(command.FlowId, command.TasksId);
            if (relacaoAlvo == null)
                return Result<object?>.Failure("Relação não encontrada");

            int ordemAtual = relacaoAlvo.Ordem;
            int novaOrdem = command.Subir ? ordemAtual - 1 : ordemAtual + 1;

            if (novaOrdem <= 0) return Result<object?>.Success(null);

            var relacaoVizinha = await _taskFlowTasksRepository.GetByOrderAsync(command.FlowId, novaOrdem);

            if (relacaoVizinha == null)
                return Result<object?>.Success(null);

            relacaoVizinha.AlterarOrdem(ordemAtual);
            relacaoAlvo.AlterarOrdem(novaOrdem);

            await _taskFlowTasksRepository.UpdateRange([relacaoAlvo, relacaoVizinha]);

            return Result<object?>.Success(null);
        }
           
    }
}
