using GateAPI.Application.Common.Models;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.AtualizarParcial
{
    public record AtualizarParcialTipoAvariaCommand(Guid Id,string? Tipo,string? Descricao,StatusEnum? Status) : IRequest<Result<Guid>>;
}