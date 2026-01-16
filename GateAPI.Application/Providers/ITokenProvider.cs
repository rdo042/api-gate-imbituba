using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Application.Providers
{
    public interface ITokenProvider
    {
        string GenerateToken(Usuario user);
    }
}
