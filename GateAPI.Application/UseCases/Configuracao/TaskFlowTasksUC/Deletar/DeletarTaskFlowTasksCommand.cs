using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.Deletar
{
    public record DeletarTaskFlowTasksCommand(Guid FlowId, Guid TasksId) : IRequest<Result<object?>>;
}
