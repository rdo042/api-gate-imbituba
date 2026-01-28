using GateAPI.Infra.Models;
using System.ComponentModel.DataAnnotations;

namespace GateAPI.Infra.Models.Configuracao
{
    public class TraducaoModel : BaseModel
    {
        public Guid IdIdioma { get; set; }

        [StringLength(100)]
        public required string Chave { get; set; }

        public required string Frase { get; set; }
    }
}
