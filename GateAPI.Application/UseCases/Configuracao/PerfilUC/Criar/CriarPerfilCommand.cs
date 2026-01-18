namespace GateAPI.Application.UseCases.Configuracao.PerfilUC.Criar
{
    public record PermissaoItem(Guid Id, string Nome);
    public record CriarPerfilCommand(
        string Nome,
        string? Descricao,
        ICollection<PermissaoItem> Permissoes)
    {
        public ICollection<PermissaoItem> Permissoes { get; init; } = Permissoes ?? [];
    }
}
