using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Criar
{
    public class CriarTipoLacreHandler(ITipoLacreRepository tipoLacre) : IRequestHandler<CriarTipoLacreCommand, Result<TipoLacre>>
    {
        private readonly ITipoLacreRepository _tipoLacreRepository = tipoLacre;

        public async Task<Result<TipoLacre>> Handle(CriarTipoLacreCommand command, CancellationToken cancellationToken = default)
        {
            var obj = new TipoLacre(command.Tipo, command.Descricao, command.Status);
            var result = await _tipoLacreRepository.AddAsync(obj);

            return Result<TipoLacre>.Success(result);
        }
    }
}
