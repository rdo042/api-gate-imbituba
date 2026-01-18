using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Criar
{
    public class CriarTipoLacreHandler(ITipoLacreRepository tipoLacre) : ICommandHandler<CriarTipoLacreCommand, Result<TipoLacre>>
    {
        private readonly ITipoLacreRepository _tipoLacreRepository = tipoLacre;

        public async Task<Result<TipoLacre>> HandleAsync(CriarTipoLacreCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var obj = new TipoLacre(command.Tipo, command.Descricao, command.Status);
                var result = await _tipoLacreRepository.AddAsync(obj);

                return Result<TipoLacre>.Success(result);

            }catch (Exception ex)
            {
                return Result<TipoLacre>.Failure("Erro ao criar tipo lacre - " + ex.Message);  
            }
        }
    }
}
