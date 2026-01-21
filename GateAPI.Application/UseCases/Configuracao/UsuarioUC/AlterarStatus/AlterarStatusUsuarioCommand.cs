using GateAPI.Domain.Enums;

namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.AlterarStatus
{
    public record AlterarStatusUsuarioCommand(Guid Id, StatusEnum Status);
}
