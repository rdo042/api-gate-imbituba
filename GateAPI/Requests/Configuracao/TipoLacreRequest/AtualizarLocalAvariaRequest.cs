using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.LocalAvariaRequest
{
    public record AtualizarLocalAvariaRequest(string Local, string Descricao, StatusEnum Status);
}
