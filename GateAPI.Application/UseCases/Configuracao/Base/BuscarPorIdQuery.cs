using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.Base
{
    public record BuscarPorIdQuery<TEntity>(Guid Id)
    : IRequest<Result<TEntity?>> where TEntity : class;

}
