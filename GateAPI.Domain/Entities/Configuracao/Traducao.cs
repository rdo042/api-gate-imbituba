namespace GateAPI.Domain.Entities.Configuracao
{
    public class Traducao
    {
        public Guid Id { get; private set; }
        public Guid IdIdioma { get; private set; }
        public string Chave { get; private set; } = string.Empty;
        public string Frase { get; private set; } = string.Empty;

        public Traducao(Guid idIdioma, string chave, string frase)
        {
            Id = Guid.NewGuid();
            IdIdioma = idIdioma;
            Chave = chave;
            Frase = frase;

            Validation();
        }

        private Traducao() { }

        public void UpdateEntity(string chave, string frase)
        {
            Chave = chave;
            Frase = frase;

            Validation();
        }

        public static Traducao Load(Guid id, Guid idIdioma, string chave, string frase)
        {
            var entidade = new Traducao
            {
                Id = id,
                IdIdioma = idIdioma,
                Chave = chave,
                Frase = frase
            };

            entidade.Validation();
            return entidade;
        }

        private void Validation()
        {
            if (IdIdioma == Guid.Empty)
                throw new ArgumentException("O ID do idioma é obrigatório.");

            if (string.IsNullOrWhiteSpace(Chave))
                throw new ArgumentException("A chave da tradução é obrigatória.");

            if (Chave.Length > 100)
                throw new ArgumentException("A chave não pode exceder 100 caracteres.");

            if (string.IsNullOrWhiteSpace(Frase))
                throw new ArgumentException("A frase da tradução é obrigatória.");
        }
    }
}
