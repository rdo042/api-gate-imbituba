using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Atualizar
{
    public class AtualizarLocalAvariaHandler(ILocalAvariaRepository localAvaria) : IRequestHandler<AtualizarLocalAvariaCommand, Result<LocalAvaria?>>
    {
        private readonly ILocalAvariaRepository _localAvariaRepository = localAvaria;
        public async Task<Result<LocalAvaria?>> Handle(AtualizarLocalAvariaCommand command, CancellationToken cancellationToken = default)
        {
            var entidade = await _localAvariaRepository.GetByIdAsync(command.Id);

            if (entidade == null)
                return Result<LocalAvaria?>.Failure("Local Avaria nao encontrado pelo id" + command.Id);

            var localAvariaExist = await _localAvariaRepository.GetByLocalAsync(command.Local);

            if (localAvariaExist != null && localAvariaExist.Id != command.Id)
                return Result<LocalAvaria?>.Failure("Já existe um Local Avaria com esse Local" + command.Local);

            entidade.UpdateEntity(command.Local, command.Decricao, command.Status);

            await _localAvariaRepository.UpdateAsync(entidade);

            return Result<LocalAvaria?>.Success(entidade);
        }
    }
}
