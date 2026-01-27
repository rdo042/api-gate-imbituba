using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.MotoristaUC.Deletar
{ 
    public record DeletarMotoristaCommand(Guid Id) : IRequest<Result<Guid>>;
}
