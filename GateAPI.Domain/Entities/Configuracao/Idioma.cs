using GateAPI.Domain.Enums;

namespace GateAPI.Domain.Entities.Configuracao
{
    public class Idioma
    {
        public Guid Id { get; private set; }
        public string Codigo { get; private set; } = string.Empty;
        public string Nome { get; private set; } = string.Empty;
        public string? Descricao { get; private set; }
        public StatusEnum Status { get; private set; }
        public CanalEnum Canal { get; private set; }
        public bool EhPadrao { get; private set; }

        public Idioma(string codigo, string nome, string? descricao, StatusEnum status, CanalEnum canal, bool ehPadrao)
        {
            Id = Guid.NewGuid();
            Codigo = codigo;
            Nome = nome;
            Descricao = descricao;
            Status = status;
            Canal = canal;
            EhPadrao = ehPadrao;

            Validation();
        }

        private Idioma() { }

        public void UpdateEntity(string codigo, string nome, string? descricao, StatusEnum status, CanalEnum canal)
        {
            Codigo = codigo;
            Nome = nome;
            Descricao = descricao;
            Status = status;
            Canal = canal;

            Validation();
        }

        public void DefinirComoPadrao()
        {
            if (this.Status != StatusEnum.ATIVO)
                throw new ArgumentException("Um idioma inativo não pode ser definido como padrão.");

            this.EhPadrao = true;
        }

        public void RemoverComoPadrao()
        {
            this.EhPadrao = false;
        }

        public void AlterarStatus(StatusEnum novoStatus)
        {
            if (this.EhPadrao && novoStatus != StatusEnum.ATIVO)
                throw new ArgumentException("Um idioma padrão não pode ser inativado.");

            this.Status = novoStatus;
        }

        public static Idioma Load(Guid id, string codigo, string nome, string? descricao, StatusEnum status, CanalEnum canal, bool ehPadrao)
        {
            var entidade = new Idioma
            {
                Id = id,
                Codigo = codigo,
                Nome = nome,
                Descricao = descricao,
                Status = status,
                Canal = canal,
                EhPadrao = ehPadrao
            };

            entidade.Validation();
            return entidade;
        }

        private void Validation()
        {
            if (string.IsNullOrWhiteSpace(this.Codigo))
                throw new ArgumentException("O Código do idioma é obrigatório.");
            if (this.Codigo.Length > 10)
                throw new ArgumentException("O Código do idioma não pode exceder 10 caracteres.");
            if (!IsValidISO639(this.Codigo))
                throw new ArgumentException("O Código deve seguir o padrão ISO BCP 47 (ex: pt-BR, en-US).");
            if (string.IsNullOrWhiteSpace(this.Nome))
                throw new ArgumentException("O Nome do idioma é obrigatório.");
            if (this.Nome.Length > 50)
                throw new ArgumentException("O Nome do idioma não pode exceder 50 caracteres.");
            if (this.Descricao?.Length > 255)
                throw new ArgumentException("A Descrição do idioma não pode exceder 255 caracteres.");
            if (this.EhPadrao && this.Status != StatusEnum.ATIVO)
                throw new ArgumentException("Um idioma padrão deve estar ATIVO.");
        }

        private static bool IsValidISO639(string codigo)
        {
            // Validação simples: deve ter padrão xx-YY (ex: pt-BR, en-US, en)
            if (string.IsNullOrEmpty(codigo))
                return false;

            var partes = codigo.Split('-');
            
            // Aceita: "pt", "en", "pt-BR", "en-US"
            if (partes.Length == 1 && partes[0].Length == 2)
                return partes[0].All(char.IsLower);
            
            if (partes.Length == 2 && partes[0].Length == 2 && partes[1].Length == 2)
                return partes[0].All(char.IsLower) && partes[1].All(char.IsUpper);

            return false;
        }
    }
}
