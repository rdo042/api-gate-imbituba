using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.BuscarPorId
{
    public record BuscarPorIdTipoLacreQuery(Guid Id) : IRequest<Result<TipoLacre>>;
}
