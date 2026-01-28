using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.TipoLacreRequest
{
    public record CriarLocalAvariaRequest(Guid Id, string Local, string Descricao, StatusEnum Status);
}
