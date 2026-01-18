namespace GateAPI.Infra.Models.Configuracao
{
    public class PermissaoModel
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public ICollection<PerfilModel> Perfis { get; set; } = [];
    }
}
