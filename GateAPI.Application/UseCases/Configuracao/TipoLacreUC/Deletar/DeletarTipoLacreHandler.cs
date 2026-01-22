using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar
{
    public class DeletarTipoLacreHandler(ITipoLacreRepository tipoLacre) : IRequestHandler<DeletarTipoLacreCommand, Result<object?>>
    {
        private readonly ITipoLacreRepository _tipoLacreRepository = tipoLacre;

        public async Task<Result<object?>> Handle(DeletarTipoLacreCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                await _tipoLacreRepository.DeleteAsync(command.Id);

                return Result<object?>.Success(command.Id);

            }
            catch (Exception ex)
            {
                return Result<object?>.Failure("Erro ao deletar tipo lacre - " + ex.Message);
            }
        }
    }
}
