using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TasksUC.BuscarTodosPorParametro
{
    public class BuscarTodosPorParametroTasksHandler(ITasksRepository tasks) : IRequestHandler<BuscarTodosPorParametroTasksQuery, Result<IEnumerable<Tasks>>>
    {
        private readonly ITasksRepository _tasksRepository = tasks;

        public async Task<Result<IEnumerable<Tasks>>> Handle(BuscarTodosPorParametroTasksQuery command, CancellationToken cancellationToken = default)
        {
            var result = await _tasksRepository.GetAllPorParametroAsync(command.Nome);

            return Result<IEnumerable<Tasks>>.Success(result);
        }
    }
}
