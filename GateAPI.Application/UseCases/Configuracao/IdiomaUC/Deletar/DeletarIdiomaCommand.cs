using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.Deletar
{
    public record DeletarIdiomaCommand(Guid Id) : IRequest<Result<string>>;
}
