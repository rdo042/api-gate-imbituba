using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Criar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Criar
{
    public class CriarLocalAvariaHandler(ILocalAvariaRepository localAvaria) : IRequestHandler<CriarLocalAvariaCommand, Result<LocalAvaria>>
    {
        private readonly ILocalAvariaRepository _localAvariaRepository = localAvaria;

        public async Task<Result<LocalAvaria>> Handle(CriarLocalAvariaCommand command, CancellationToken cancellationToken = default)
        {
            var localAvariaExist = await _localAvariaRepository.GetByLocalAsync(command.Local);

            if (localAvariaExist != null)
                return Result<LocalAvaria>.Failure("Já existe um Local Avaria com esse Local" + command.Local);

            var obj = new LocalAvaria(command.Local, command.Descricao, command.Status);
            var result = await _localAvariaRepository.AddAsync(obj);

            return Result<LocalAvaria>.Success(result);
        }
    }
}
