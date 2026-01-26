using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.Criar
{
    public record CriarIdiomaCommand(
        string Codigo,
        string Nome,
        string? Descricao,
        StatusEnum Status,
        CanalEnum Canal,
        bool EhPadrao) : IRequest<Result<Idioma>>;
}
