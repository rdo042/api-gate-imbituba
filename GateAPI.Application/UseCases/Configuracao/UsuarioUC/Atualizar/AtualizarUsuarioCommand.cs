using GateAPI.Domain.Enums;

namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.Atualizar
{
    public record AtualizarUsuarioCommand(Guid Id, string Nome, string Email, string? Foto, Guid PerfilId, StatusEnum Status);
}
