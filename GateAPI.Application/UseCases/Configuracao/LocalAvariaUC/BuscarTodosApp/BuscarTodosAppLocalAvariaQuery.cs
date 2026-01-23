using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.BuscarTodosApp
{
    public record BuscarTodosAppLocalAvariaQuery : IRequest<Result<IEnumerable<LocalAvaria>>>;
}
