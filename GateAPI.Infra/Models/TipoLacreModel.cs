using GateAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GateAPI.Infra.Models
{
    public class TipoLacreModel : BaseModel
    {
        [StringLength(50)] public required string Tipo { get; set; }
        [StringLength(300)] public string? Descricao { get; set; }
        public StatusEnum Status { get; set; }
    }
}
