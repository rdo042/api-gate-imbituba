using GateAPI.Domain.Enums;

namespace GateAPI.Infra.Models.Configuracao
{
    public class TasksModel : BaseModel
    {
        public string Nome { get; set; }
        public string Url { get; set; }
        public StatusEnum Status { get; set; }
    }
}
