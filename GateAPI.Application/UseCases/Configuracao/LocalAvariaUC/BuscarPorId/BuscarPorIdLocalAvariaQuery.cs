using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.BuscarPorId
{
    public record BuscarPorIdLocalAvariaQuery(Guid Id) : IRequest<Result<LocalAvaria>>;
}
