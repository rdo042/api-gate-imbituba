using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC
{
    public record BuscarTodosTipoAvariaQuery(int? PageNumber, int? PageSize) : IRequest<Result<PaginatedResultDto<TipoAvaria?>>>;
}