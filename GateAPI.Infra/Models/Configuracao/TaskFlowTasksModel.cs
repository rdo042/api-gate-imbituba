using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;

namespace GateAPI.Infra.Models.Configuracao
{
    public class TaskFlowTasksModel
    {
        public Guid Id { get; set; }
        public Guid TaskFlowId { get; set; }
        public TaskFlow TaskFlow { get; set; }

        public Guid TasksId { get; set; }
        public Tasks Tasks { get; set; }

        public int Ordem { get; set; }
        public StatusEnum Status { get; set; }
    }
}
