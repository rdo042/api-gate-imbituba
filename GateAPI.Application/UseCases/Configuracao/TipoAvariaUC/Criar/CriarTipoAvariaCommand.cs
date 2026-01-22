using GateAPI.Application.Common.Models;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar
{
    public class CriarTipoAvariaCommand: IRequest<Result<Guid>>
    {
        public Domain.Entities.Configuracao.TipoAvaria TipoAvaria { get; }

        public CriarTipoAvariaCommand(Domain.Entities.Configuracao.TipoAvaria tpAvaria)
        {
            TipoAvaria = tpAvaria;
        }
    }


}
