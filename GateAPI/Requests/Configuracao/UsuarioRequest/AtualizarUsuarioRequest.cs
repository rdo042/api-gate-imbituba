using GateAPI.Domain.Enums;

namespace GateAPI.Requests.Configuracao.UsuarioRequest
{
    public record AtualizarUsuarioRequest(string Nome, string Email, string Senha, string? LinkFoto, Guid PerfilId, StatusEnum Status);
}
