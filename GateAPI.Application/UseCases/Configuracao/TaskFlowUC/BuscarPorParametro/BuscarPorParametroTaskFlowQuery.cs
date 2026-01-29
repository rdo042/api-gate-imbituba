using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowUC.BuscarPorParametro
{
    public record BuscarPorParametroTaskFlowQuery(Guid Id) : IRequest<Result<TaskFlow>>;
}
