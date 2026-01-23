using GateAPI.Application.Common.Models;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar
{
    public record AtualizarTipoAvariaCommand(Guid Id,string Tipo,string? Descricao,StatusEnum Status) : IRequest<Result<Guid>>;
}