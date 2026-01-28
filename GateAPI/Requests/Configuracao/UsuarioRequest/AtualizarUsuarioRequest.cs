using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.UsuarioRequest
{
    public record AtualizarUsuarioRequest(string Nome, string Email, string? Foto, Guid PerfilId, StatusEnum Status);
}
