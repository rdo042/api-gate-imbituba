using GateAPI.Domain.Enums;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Criar
{
    public record CriarTipoLacreCommand(
        string Tipo,
        string? Descricao,
        StatusEnum Status);
}
