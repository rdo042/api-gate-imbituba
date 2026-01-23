using GateAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GateAPI.Infra.Models.Configuracao
{
    public class TipoAvariaModel : BaseModel
    {
        [StringLength(50)] public string Tipo { get; set; }
        [StringLength(300)] public string? Descricao { get; set; }
        public StatusEnum Status { get; set; }
    }
}
