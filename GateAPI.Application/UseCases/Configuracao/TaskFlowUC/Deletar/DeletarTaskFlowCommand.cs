using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowUC.Deletar
{
    public record DeletarTaskFlowCommand(Guid Id) : IRequest<Result<object?>>;
}
