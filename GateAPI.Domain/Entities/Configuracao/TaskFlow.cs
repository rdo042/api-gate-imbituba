namespace GateAPI.Domain.Entities.Configuracao
{
    public class TaskFlow : BaseEntity
    {
        public string Nome { get; private set; }
        public IEnumerable<Tasks> Tasks { get; private set; }

        public TaskFlow(string nome, IEnumerable<Tasks> tasks) {
            Id = Guid.NewGuid();
            Nome = nome;
            Tasks = tasks;

            Validation();
        }

        private TaskFlow() { }

        public static TaskFlow Load(Guid id, string nome, IEnumerable<Tasks> tasks)
        {
            var entidade = new TaskFlow
            {
                Nome = nome,
                Tasks = tasks
            };

            entidade.SetId(id);

            return entidade;
        }

        private void Validation()
        {
            if (string.IsNullOrWhiteSpace(this.Nome))
                throw new ArgumentException("O Nome do TaskFlow é obrigatório.");
            if (this.Nome.Length > 100)
                throw new ArgumentException("O tamanho do nome do TaskFlow não é suportado - 100");
        }
    }
}
