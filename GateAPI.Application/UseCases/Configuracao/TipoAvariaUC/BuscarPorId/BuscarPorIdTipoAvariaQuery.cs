using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC
{
    public record BuscarPorIdTipoAvariaQuery(Guid Id) : IRequest<Result<Domain.Entities.Configuracao.TipoAvaria?>>;
}