namespace GateAPI.Domain.Entities.Configuracao
{
    public class Permissao
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;

        private Permissao() { }

        public static Permissao Load(Guid id, string nome)
        {
            return new Permissao() { 
                Id = id,
                Nome = nome 
            };
        }
    }
}
