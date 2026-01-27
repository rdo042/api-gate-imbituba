using GateAPI.Domain.Enums;

namespace GateAPI.Domain.Entities.Configuracao
{
    public class TaskFlowTasks
    {
        public Guid Id { get; private set; }
        public TaskFlow TaskFlow { get; private set; }
        public Tasks Tasks { get; private set; }
        public int Ordem {  get; private set; }
        public StatusEnum Status { get; private set; }

        public TaskFlowTasks(TaskFlow taskFlow, Tasks task, int ordem, StatusEnum status)
        {
            Id = Guid.NewGuid();
            TaskFlow = taskFlow;
            Tasks = task;
            Ordem = ordem;
            Status = status;

            Validation();
        }

        private void Validation()
        {
            if (Ordem < 1)
                throw new ArgumentException("A ordem não pode ser negativa.");
        }
    }
}
