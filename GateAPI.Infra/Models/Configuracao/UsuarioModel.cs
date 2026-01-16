using GateAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GateAPI.Infra.Models.Configuracao
{
    public class UsuarioModel : BaseModel
    {
        [StringLength(50)] public required string Nome { get; set; }
        [StringLength(50)] public required string Email { get; set; }
        [StringLength(50)] public required string SenhaHash { get; set; }
        public Guid PerfilId { get; set; }
        public PerfilModel Perfil { get; set; } = default!;
        public StatusEnum Status { get; set; }
    }
}
