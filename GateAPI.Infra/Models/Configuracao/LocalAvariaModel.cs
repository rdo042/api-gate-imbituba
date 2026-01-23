using GateAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GateAPI.Infra.Models.Configuracao
{
    public class LocalAvariaModel : BaseModel
    {
        [StringLength(100)] public required string Local { get; set; }
        [StringLength(255)] public required string Descricao { get; set; }
        public StatusEnum Status { get; set; }
    }
}
