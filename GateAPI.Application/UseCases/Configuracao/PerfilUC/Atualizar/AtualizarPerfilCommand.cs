namespace GateAPI.Application.UseCases.Configuracao.PerfilUC.Atualizar
{
    public record PermissaoItem(Guid Id, string Nome);
    public record AtualizarPerfilCommand(
        Guid Id,
        string Nome,
        string? Descricao,
        ICollection<PermissaoItem> Permissoes)
    {
        public ICollection<PermissaoItem> Permissoes { get; init; } = Permissoes ?? [];
    }
}
