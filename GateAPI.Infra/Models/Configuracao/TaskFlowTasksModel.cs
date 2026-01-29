using GateAPI.Domain.Enums;

namespace GateAPI.Infra.Models.Configuracao
{
    public class TaskFlowTasksModel
    {
        public Guid Id { get; set; }
        public Guid TaskFlowId { get; set; }
        public TaskFlowModel TaskFlow { get; set; }

        public Guid TasksId { get; set; }
        public TasksModel Tasks { get; set; }

        public int Ordem { get; set; }
        public StatusEnum Status { get; set; }
    }
}
