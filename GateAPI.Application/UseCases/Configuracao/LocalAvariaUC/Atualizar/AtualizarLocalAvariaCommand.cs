using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Atualizar
{
    public record AtualizarLocalAvariaCommand(
        Guid Id,
        string Local,
        string Decricao,
        StatusEnum Status) : IRequest<Result<LocalAvaria?>>;
}
