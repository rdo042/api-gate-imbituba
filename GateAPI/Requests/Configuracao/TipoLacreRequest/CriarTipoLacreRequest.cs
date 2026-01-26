using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.TipoLacreRequest
{
    public record CriarTipoLacreRequest(string Nome, string? Descricao, StatusEnum Status);
}
