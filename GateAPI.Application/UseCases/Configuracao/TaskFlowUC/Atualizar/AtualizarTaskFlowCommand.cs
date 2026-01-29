using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowUC.Atualizar
{
    public record AtualizarTaskFlowCommand(Guid Id, string Nome) : IRequest<Result<object?>>;
}
