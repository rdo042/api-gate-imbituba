using GateAPI.Domain.Enums;
using GateAPI.Infra.Models;
using System.ComponentModel.DataAnnotations;

namespace GateAPI.Infra.Models.Configuracao
{
    public class IdiomaModel : BaseModel
    {
        [StringLength(10)] 
        public required string Codigo { get; set; }
        [StringLength(50)] 
        public required string Nome { get; set; }
        [StringLength(255)] 
        public string? Descricao { get; set; }
        public StatusEnum Status { get; set; }
        public int Canal { get; set; }
        public bool EhPadrao { get; set; }
    }
}
