using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowUC.Criar
{
    public record CriarTaskFlowCommand(string Nome) : IRequest<Result<TaskFlow>>;
}
