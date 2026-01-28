using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.MotoristaUC.BuscarTodos
{
    public record BuscarTodosMotoristaQuery(int? PageNumber, int? PageSize) : IRequest<Result<PaginatedResultDto<Motorista?>>>;
}