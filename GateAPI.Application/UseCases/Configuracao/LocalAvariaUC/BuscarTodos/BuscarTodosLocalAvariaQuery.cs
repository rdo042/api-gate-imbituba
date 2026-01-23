using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.BuscarTodos
{
    public record BuscarTodosLocalAvariaQuery : IRequest<Result<IEnumerable<LocalAvaria>>>;
}
