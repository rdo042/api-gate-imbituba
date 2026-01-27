namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.Criar
{
    public record CriarUsuarioCommand(string Nome, string Email, string Senha, string? Foto, Guid PerfilId);
}
