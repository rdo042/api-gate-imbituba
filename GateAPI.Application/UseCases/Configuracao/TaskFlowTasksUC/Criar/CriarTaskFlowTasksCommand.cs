using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.Criar
{
    public record CriarTaskFlowTasksCommand(Guid FlowId, Guid TasksId) : IRequest<Result<int>>;
}
