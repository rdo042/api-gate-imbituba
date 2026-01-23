using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.LocalAvariaRequest
{
    public record CriarLocalAvariaRequest(Guid Id, string Local, string Descricao, StatusEnum Status);
}
