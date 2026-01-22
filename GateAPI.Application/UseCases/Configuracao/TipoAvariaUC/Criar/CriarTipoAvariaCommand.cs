using GateAPI.Application.Common.Models;
using GateAPI.Domain.Enums;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar
{
    public class CriarTipoAvariaCommand: IRequest<Result<Guid>>
    {
        public string Tipo { get; set; } 
        public string? Descricao { get; set; }
        public StatusEnum Status { get; set; }
    }
}
