using GateAPI.Domain.Enums;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar
{
    public record AtualizarTipoLacreCommand(
        Guid Id,
        string Tipo,
        string? Decricao,
        StatusEnum Status);
}
