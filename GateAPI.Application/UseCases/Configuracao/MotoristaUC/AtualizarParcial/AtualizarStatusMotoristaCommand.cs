using GateAPI.Application.Common.Models;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.MotoristaUC.AtualizarParcial
{
    public record AtualizarStatusMotoristaCommand(Guid Id, StatusEnum Status) : IRequest<Result<StatusEnum>>;

}
