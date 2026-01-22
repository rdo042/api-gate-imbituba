using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar
{
    public class AtualizarTipoLacreHandler(ITipoLacreRepository tipoLacre) : IRequestHandler<AtualizarTipoLacreCommand, Result<object?>>
    {
        private readonly ITipoLacreRepository _tipoLacreRepository = tipoLacre;

        public async Task<Result<object?>> Handle(AtualizarTipoLacreCommand command, CancellationToken cancellationToken = default)
        {
            var entidade = await _tipoLacreRepository.GetByIdAsync(command.Id);

            if (entidade == null)
                return Result<object?>.Failure("TipoLacre nao encontrado pelo id" + command.Id);

            entidade.UpdateEntity(command.Tipo, command.Decricao, command.Status);

            await _tipoLacreRepository.UpdateAsync(entidade);

            return Result<object?>.Success(entidade);
        }
    }
}
