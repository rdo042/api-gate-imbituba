using GateAPI.Domain.Entities.Configuracao;
using System.ComponentModel.DataAnnotations;

namespace GateAPI.Infra.Models.Configuracao
{
    public class TaskFlowModel : BaseModel
    {
        [MaxLength(100)] public string Nome { get; set; }
        public ICollection<TaskFlowTasksModel> TaskFlowTasks { get; set; } = [];
    }
}
