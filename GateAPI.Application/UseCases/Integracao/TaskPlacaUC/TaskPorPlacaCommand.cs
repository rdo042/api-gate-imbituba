using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Integracao;
using MediatR;

namespace GateAPI.Application.UseCases.Integracao.ReconhecerPlacaUC
{
    public record TaskPorPlacaCommand(
        string placa) : IRequest<Result<TaskFlowPlateResponse>>;
}
