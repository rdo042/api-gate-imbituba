using GateAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GateAPI.Infra.Models.Configuracao
{
    public class UsuarioModel : BaseModel
    {
        [StringLength(100)] public required string Nome { get; set; }
        [StringLength(150)] public required string Email { get; set; }
        [StringLength(255)] public required string SenhaHash { get; set; }
        public bool EmailConfirmado { get; set; }
        public string? LinkFoto { get; set; }
        public Guid? PerfilId { get; set; }
        public PerfilModel? Perfil { get; set; }
        public StatusEnum Status { get; set; }
    }
}
