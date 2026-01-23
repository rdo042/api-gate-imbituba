using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Criar
{
    public record CriarLocalAvariaCommand(
        string Local,
        string Descricao,
        StatusEnum Status) : IRequest<Result<LocalAvaria>>;
}
