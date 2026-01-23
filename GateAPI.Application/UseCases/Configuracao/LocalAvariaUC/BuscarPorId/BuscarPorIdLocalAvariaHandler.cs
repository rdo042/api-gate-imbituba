using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.BuscarPorId;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.BuscarPorId
{
    public class BuscarPorIdLocalAvariaHandler(ILocalAvariaRepository localAvaria) : IRequestHandler<BuscarPorIdLocalAvariaQuery, Result<LocalAvaria>>
    {
        private readonly ILocalAvariaRepository _localAvariaRepository = localAvaria;

        public async Task<Result<LocalAvaria>> Handle(BuscarPorIdLocalAvariaQuery command, CancellationToken cancellationToken)
        {
            var localAvaria = await _localAvariaRepository.GetByIdAsync(command.Id);

            if (localAvaria is null)
                return Result<LocalAvaria>.Failure($"Local de avaria não encontrado {command.Id}");

            return Result<LocalAvaria>.Success(localAvaria);
        }
    }
}
