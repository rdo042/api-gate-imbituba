using GateAPI.Domain.Enums;

namespace GateAPI.Domain.Entities.Configuracao
{
    public class TipoLacre : BaseEntity
    {
        public string Tipo { get; private set; } = string.Empty;
        public string? Descricao { get; private set; }
        public StatusEnum Status { get; private set; }

        public TipoLacre(string tipo, string? descricao, StatusEnum status) 
        {
            Id = Guid.NewGuid();
            Tipo = tipo;
            Descricao = descricao;
            Status = status;

            Validation();
        }

        private TipoLacre() { }

        public void UpdateEntity(string tipo, string? descricao, StatusEnum status)
        {
            Tipo = tipo;
            Descricao = descricao;
            Status = status;

            Validation();
        }

        public static TipoLacre Load(Guid id, string tipo, string? descricao, StatusEnum status)
        {
            var entidade = new TipoLacre
            {
                Tipo = tipo,
                Descricao = descricao,
                Status = status
            };

            entidade.SetId(id);
            //entidade.SetAudit();
            return entidade;
        }

        private void Validation()
        {
            if (string.IsNullOrWhiteSpace(this.Tipo))
                throw new ArgumentException("O Tipo do lacre é obrigatório.");
            if (this.Tipo.Length > 50)
                throw new ArgumentException("O tamanho do Tipo não é suportado");
            if (this.Descricao?.Length > 300)
                throw new ArgumentException("O tamanho da Descrição não é suportado");
        }
    }
}
