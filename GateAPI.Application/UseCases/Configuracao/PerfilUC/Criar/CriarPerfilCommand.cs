using GateAPI.Application.Common.DTOs;

namespace GateAPI.Application.UseCases.Configuracao.PerfilUC.Criar
{
    
    public record CriarPerfilCommand(
        string Nome,
        string? Descricao,
        ICollection<PermissaoItem> Permissoes)
    {
        public ICollection<PermissaoItem> Permissoes { get; init; } = Permissoes ?? [];
    }
}
