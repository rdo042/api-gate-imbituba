using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.BuscarPorId
{
    public class BuscarPorIdTipoLacreHandler(ITipoLacreRepository tipoLacre) : ICommandHandler<BuscarPorIdTipoLacreQuery, Result<TipoLacre>>
    {
        private readonly ITipoLacreRepository _tipoLacreRepository = tipoLacre;

        public async Task<Result<TipoLacre>> HandleAsync(BuscarPorIdTipoLacreQuery command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _tipoLacreRepository.GetByIdAsync(command.Id)
                    ?? throw new NullReferenceException("Nao encontrado pelo id " + command.Id);

                return Result<TipoLacre>.Success(result);

            }
            catch (Exception ex)
            {
                return Result<TipoLacre>.Failure("Erro ao buscar tipo lacre - " + ex.Message);
            }
        }
    }
}
