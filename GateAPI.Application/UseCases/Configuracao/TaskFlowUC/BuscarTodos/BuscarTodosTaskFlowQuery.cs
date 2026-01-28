using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TaskFlowUC.BuscarTodos
{
    public record BuscarTodosTaskFlowQuery() : IRequest<Result<IEnumerable<TaskFlow>>>;
}
