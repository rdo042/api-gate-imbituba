using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.BuscarPorId
{
    public class BuscarPorIdTipoLacreHandler(ITipoLacreRepository tipoLacre) : IRequestHandler<BuscarPorIdTipoLacreQuery, Result<TipoLacre>>
    {
        private readonly ITipoLacreRepository _tipoLacreRepository = tipoLacre;

        public async Task<Result<TipoLacre>> Handle(BuscarPorIdTipoLacreQuery command, CancellationToken cancellationToken = default)
        {
            var result = await _tipoLacreRepository.GetByIdAsync(command.Id);

            if (result is null)
                return Result<TipoLacre>.Failure("Nao encontrado pelo id " + command.Id);

            return Result<TipoLacre>.Success(result);
        }
    }
}
