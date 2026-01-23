using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar
{
    public record CriarTipoAvariaCommand(string Tipo, string? Descricao, StatusEnum Status = StatusEnum.ATIVO) : IRequest<Result<TipoAvaria>>;
}