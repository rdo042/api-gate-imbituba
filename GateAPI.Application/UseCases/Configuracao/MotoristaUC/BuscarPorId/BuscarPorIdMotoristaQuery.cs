using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC
{
    public record BuscarPorIdMotoristaQuery(Guid Id) : IRequest<Result<Motorista?>>;
}