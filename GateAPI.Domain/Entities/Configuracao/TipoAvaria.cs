using GateAPI.Domain.Enums;

namespace GateAPI.Domain.Entities.Configuracao
{
    public class TipoAvaria : BaseEntity
    {
        public string Tipo { get; private set; } = string.Empty;
        public string Descricao { get; private set; }
        public StatusEnum Status { get; private set; }

        public TipoAvaria(string tipo, string descricao, StatusEnum status = StatusEnum.ATIVO) 
        {
            Id = Guid.NewGuid();
            Tipo = tipo;
            Descricao = descricao;
            Status = status;

            Validation();
        }

        public void UpdateEntity(string tipo, string descricao, StatusEnum status = StatusEnum.ATIVO)
        {
            Tipo = tipo;
            Descricao = descricao;
            Status = status;

            Validation();
        }

        public static TipoAvaria Load(Guid id, string tipo, string descricao, StatusEnum status)
        {
            var entidade = new TipoAvaria(tipo, descricao, status);
            entidade.SetId(id);
            
            return entidade;
        }

        private void Validation()
        {
            if (string.IsNullOrWhiteSpace(this.Tipo))
                throw new ArgumentException("O Tipo do avaria é obrigatório.");
            if (this.Tipo.Length > 50)
                throw new ArgumentException("O tamanho do Tipo não é suportado");
            if (this.Descricao?.Length > 300)
                throw new ArgumentException("O tamanho da Descrição não é suportado");
        }
    }
}
