using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.BuscarTodos
{
    public record BuscarTodosIdiomaQuery() : IRequest<Result<IEnumerable<Idioma>>>;
}
