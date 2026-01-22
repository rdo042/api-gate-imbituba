using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.LocalAvariaRequest
{
    public record AtualizarLocalAvariaRequest(Guid Id, string Local, string Descricao, StatusEnum Status);
}
