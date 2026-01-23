using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC
{
    public record BuscarTodosTipoAvariaQuery(int? PageNumber, int? PageSize) : IRequest<Result<PaginatedResultDto<Domain.Entities.Configuracao.TipoAvaria?>>>;
}