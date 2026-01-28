using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.TipoLacreRequest
{
    public record AtualizarLocalAvariaRequest(string Local, string Descricao, StatusEnum Status);
}
