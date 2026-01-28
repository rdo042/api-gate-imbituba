using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.MotoristaUC.BuscarPorDocumento
{
    public record BuscarPorDocumentoMotoristaQuery(string documento) : IRequest<Result<Motorista?>>;
}