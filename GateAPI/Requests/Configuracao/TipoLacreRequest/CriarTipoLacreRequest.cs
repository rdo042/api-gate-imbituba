using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.TipoLacreRequest
{
    public record CriarTipoLacreRequest(Guid Id, string Nome, string? Descricao, StatusEnum Status);
}
