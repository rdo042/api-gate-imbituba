using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.TipoLacreRequest
{
    public record AtualizarTipoLacreRequest(Guid Id, string Nome, string? Descricao, StatusEnum Status);
}
