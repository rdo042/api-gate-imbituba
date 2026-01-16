using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar
{
    public class DeletarTipoLacreHandler(ITipoLacreRepository tipoLacre) : ICommandHandler<DeletarTipoLacreCommand, Result<Guid>>
    {
        private readonly ITipoLacreRepository _tipoLacreRepository = tipoLacre;

        public async Task<Result<Guid>> HandleAsync(DeletarTipoLacreCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                await _tipoLacreRepository.DeleteAsync(command.Id);

                return Result<Guid>.Success(command.Id);

            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure("Erro ao deletar tipo lacre - " + ex.Message);
            }
        }
    }
}
