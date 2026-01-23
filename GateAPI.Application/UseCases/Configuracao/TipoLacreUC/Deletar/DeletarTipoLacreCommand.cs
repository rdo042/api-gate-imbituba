using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar
{ 
    public record DeletarTipoLacreCommand(Guid Id) : IRequest<Result<object?>>;
}
