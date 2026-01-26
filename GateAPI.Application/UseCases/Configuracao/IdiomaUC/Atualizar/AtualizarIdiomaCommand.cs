using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.Atualizar
{
    public record AtualizarIdiomaCommand(
        Guid Id,
        string Codigo,
        string Nome,
        string? Descricao,
        StatusEnum Status,
        CanalEnum Canal) : IRequest<Result<string>>;
}
