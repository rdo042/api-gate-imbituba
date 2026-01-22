using GateAPI.Application.Common.Models;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar
{
    public record AtualizarTipoLacreCommand(
        Guid Id,
        string Tipo,
        string? Decricao,
        StatusEnum Status) : IRequest<Result<object?>>;
}
