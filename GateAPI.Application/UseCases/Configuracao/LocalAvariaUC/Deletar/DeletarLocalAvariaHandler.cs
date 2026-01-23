using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Deletar
{
    public class DeletarLocalAvariaHandler(ILocalAvariaRepository localAvaria) : IRequestHandler<DeletarLocalAvariaCommand, Result<bool?>>
    {
        private readonly ILocalAvariaRepository _localAvariaRepository = localAvaria;

        public async Task<Result<bool?>> Handle(DeletarLocalAvariaCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                await _localAvariaRepository.DeleteAsync(command.Id);

                return Result<bool?>.Success(true);

            }
            catch (Exception ex)
            {
                return Result<bool?>.Failure("Erro ao deletar tipo lacre - " + ex.Message);
            }
        }
    }
}
