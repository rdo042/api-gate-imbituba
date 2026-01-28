using GateAPI.Application.Common.Models;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.AlterarStatus
{
    public record AlterarStatusTaskFlowTasksCommand(Guid FlowId, Guid TasksId, StatusEnum Status) : IRequest<Result<object?>>;
}
