using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TasksUC.BuscarTodosPorParametro
{
    public record BuscarTodosPorParametroTasksQuery(string? Nome) : IRequest<Result<IEnumerable<Tasks>>>;
}
