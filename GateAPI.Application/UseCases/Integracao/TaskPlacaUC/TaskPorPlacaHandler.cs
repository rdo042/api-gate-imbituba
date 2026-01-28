using GateAPI.Application.Common.Models;
using GateAPI.Application.Providers;
using GateAPI.Domain.Entities.Integracao;
using MediatR;

namespace GateAPI.Application.UseCases.Integracao.ReconhecerPlacaUC
{
    internal class TaskPorPlacaHandler(IExternalTaskFlowProvider taskFlowProvider) : IRequestHandler<TaskPorPlacaCommand, Result<TaskFlowPlateResponse>>
    {
        private readonly IExternalTaskFlowProvider _taskFlowProvider = taskFlowProvider;

        public async Task<Result<TaskFlowPlateResponse>> Handle(TaskPorPlacaCommand command, CancellationToken ct)
        {
            var result = await _taskFlowProvider.GetTasksByLicensePlateAsync(command.placa, ct);

            if (result is null)
                return Result<TaskFlowPlateResponse>.Failure("Erro ao reconhecer placa");

            return Result<TaskFlowPlateResponse>.Success(result);
        }
    }
}
