using GateAPI.Domain.Enums;

namespace GateAPI.Domain.Entities.Configuracao
{
    public class LocalAvaria : BaseEntity
    {
        public string Local { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public StatusEnum Status { get; private set; }

        public LocalAvaria(string local, string descricao, StatusEnum status)
        {
            Id = Guid.NewGuid();
            Local = local;
            Descricao = descricao;
            Status = status;

            Validation();
        }

        private LocalAvaria() { }

        public void UpdateEntity(string? local = null, string? descricao = null, StatusEnum? status = null)
        {
            Local = local ?? Local;
            Descricao = descricao ?? Descricao;
            Status = status ?? Status;

            Validation();
        }

        public static LocalAvaria Load(Guid id, string local, string descricao, StatusEnum status)
        {
            var entidade = new LocalAvaria
            {
                Local = local,
                Descricao = descricao,
                Status = status
            };

            entidade.SetId(id);
            entidade.Validation();
            return entidade;
        }

        private void Validation()
        {
            if (string.IsNullOrWhiteSpace(this.Local))
                throw new ArgumentException("O Local é obrigatório.");
            if (this.Local.Length > 100)
                throw new ArgumentException("O tamanho do Local não é suportado");
            if (string.IsNullOrWhiteSpace(this.Descricao))
                throw new ArgumentException("A Descrição é obrigatória.");
            if (this.Descricao.Length > 255)
                throw new ArgumentException("O tamanho da Descrição não é suportado");
        }
    }
}
