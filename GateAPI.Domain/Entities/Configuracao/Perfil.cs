using GateAPI.Domain.Enums;

namespace GateAPI.Domain.Entities.Configuracao
{
    public class Perfil : BaseEntity
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public StatusEnum Status { get; set; }

        public Perfil(string nome, string? descricao) { 
            Nome = nome;
            Descricao = descricao;

            Validation();
        }

        private Perfil() { }

        public void UpdateEntity(string nome, string? descricao, StatusEnum statusEnum)
        {
            Nome = nome;
            Descricao = descricao;
            Status = statusEnum;

            Validation();
        }

        public static Perfil Load(Guid id, string nome, string? descricao, StatusEnum status)
        {
            var entidade = new Perfil
            {
                Nome = nome,
                Descricao = descricao,
                Status = status
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
