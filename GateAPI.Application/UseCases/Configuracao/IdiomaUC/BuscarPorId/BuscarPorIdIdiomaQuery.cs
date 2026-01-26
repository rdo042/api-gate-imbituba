using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.BuscarPorId
{
    public record BuscarPorIdIdiomaQuery(Guid Id) : IRequest<Result<Idioma>>;
}
