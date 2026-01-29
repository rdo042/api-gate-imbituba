using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.AlterarOrdem
{
    public record AlterarOrdemTaskFlowTasksCommand(Guid FlowId, Guid TasksId, bool Subir) : IRequest<Result<object?>>;
}
