using GateAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GateAPI.Infra.Models.Configuracao
{
    public class TasksModel : BaseModel
    {
        [MaxLength(100)] public string Nome { get; set; }
        [MaxLength(255)] public string Url { get; set; }
        public StatusEnum Status { get; set; }
    }
}
