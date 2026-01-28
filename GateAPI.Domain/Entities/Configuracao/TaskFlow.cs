namespace GateAPI.Domain.Entities.Configuracao
{
    public class TaskFlow : BaseEntity
    {
        public string Nome { get; private set; }
        public IEnumerable<TaskFlowTasks> Tasks { get; private set; }

        public TaskFlow(string nome) {
            Id = Guid.NewGuid();
            Nome = nome;

            Validation();
        }

        private TaskFlow() { }

        public void UpdateEntity(string nome)
        {
            Nome = nome;
            Validation();
        }

        public void AddTasks(IEnumerable<TaskFlowTasks> lista)
        {
            Tasks = lista;
        }

        public static TaskFlow Load(Guid id, string nome)
        {
            var entidade = new TaskFlow
            {
                Nome = nome
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
