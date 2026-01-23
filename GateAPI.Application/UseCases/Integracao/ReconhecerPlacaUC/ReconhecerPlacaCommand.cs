using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Integracao;
using MediatR;

namespace GateAPI.Application.UseCases.Integracao.ReconhecerPlacaUC
{
    public record ReconhecerPlacaCommand(
        string ImageBase64) : IRequest<Result<Lpr>>;
}
