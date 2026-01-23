using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.TipoLacreRequest
{
    public record AtualizarTipoLacreRequest(string Nome, string? Descricao, StatusEnum Status);
}
