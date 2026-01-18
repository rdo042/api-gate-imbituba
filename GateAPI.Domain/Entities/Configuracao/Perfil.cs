using GateAPI.Domain.Enums;

namespace GateAPI.Domain.Entities.Configuracao
{
    public class Perfil : BaseEntity
    {
        public string Nome { get; private set; } = string.Empty;
        public string? Descricao { get; private set; }
        public StatusEnum Status { get; private set; }
        public ICollection<Permissao> Permissoes { get; private set; } = [];

        public Perfil(string nome, string? descricao, ICollection<Permissao> permissoes) { 
            Nome = nome;
            Descricao = descricao;
            Permissoes  = permissoes;

            Validation();
        }

        private Perfil() { }

        public void UpdateEntity(string nome, string? descricao, StatusEnum statusEnum, ICollection<Permissao> permissoes)
        {
            Nome = nome;
            Descricao = descricao;
            Status = statusEnum;
            Permissoes = permissoes;

            Validation();
        }

        public static Perfil Load(Guid id, string nome, string? descricao, StatusEnum status, ICollection<Permissao> permissoes)
        {
            var entidade = new Perfil
            {
                Nome = nome,
                Descricao = descricao,
                Status = status,
                Permissoes = permissoes
            };

            entidade.SetId(id);
            //entidade.SetAudit();
            return entidade;
        }

        private void Validation()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new ArgumentException("O Nome do perfil é obrigatório.");
            if (Descricao?.Length > 300)
                throw new ArgumentException("Tamanho da Descrição não suportado.");
        }
    }
}
