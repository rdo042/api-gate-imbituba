using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.DefinirPadrao
{
    public record DefinirIdiomasPadraoCommand(Guid Id) : IRequest<Result<string>>;
}
