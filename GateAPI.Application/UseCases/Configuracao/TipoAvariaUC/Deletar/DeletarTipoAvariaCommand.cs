using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Deletar
{ 
    public record DeletarTipoAvariaCommand(Guid Id) : IRequest<Result<Guid>>;
}
