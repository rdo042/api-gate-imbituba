using GateAPI.Application.Common.Models;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.AtualizarParcial
{
    public record AtualizarParcialTipoAvariaCommand(Guid Id, string? Tipo=null ,string? Descricao = null,StatusEnum? Status= null) : IRequest<Result<Guid>>;
}