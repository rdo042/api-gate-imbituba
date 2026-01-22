using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Deletar
{ 
    public record DeletarLocalAvariaCommand(Guid Id) : IRequest<Result<bool?>>;
}
