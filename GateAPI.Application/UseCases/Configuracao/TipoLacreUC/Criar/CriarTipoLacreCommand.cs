using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Criar
{
    public record CriarTipoLacreCommand(
        string Tipo,
        string? Descricao,
        StatusEnum Status) : IRequest<Result<TipoLacre>>;
}
