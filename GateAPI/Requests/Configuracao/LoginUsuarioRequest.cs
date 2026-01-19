using System.Diagnostics.CodeAnalysis;

namespace GateAPI.Requests.Configuracao
{
    public class LoginUsuarioRequest
    {
        [NotNull] public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
