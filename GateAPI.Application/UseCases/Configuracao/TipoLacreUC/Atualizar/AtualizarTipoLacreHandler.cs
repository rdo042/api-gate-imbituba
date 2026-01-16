using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar
{
    public class AtualizarTipoLacreHandler(ITipoLacreRepository tipoLacre) : ICommandHandler<AtualizarTipoLacreCommand, Result<TipoLacre>>
    {
        private readonly ITipoLacreRepository _tipoLacreRepository = tipoLacre;

        public async Task<Result<TipoLacre>> HandleAsync(AtualizarTipoLacreCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var entidade = await _tipoLacreRepository.GetByIdAsync(command.Id) 
                    ?? throw new NullReferenceException("TipoLacre nao encontrado pelo id" + command.Id);

                entidade.UpdateEntity(command.Tipo, command.Decricao, command.Status);

                await _tipoLacreRepository.UpdateAsync(entidade);

                return Result<TipoLacre>.Success(entidade);

            }
            catch (Exception ex)
            {
                return Result<TipoLacre>.Failure("Erro ao atualizar tipo lacre - " + ex.Message);
            }
        }
    }
}
