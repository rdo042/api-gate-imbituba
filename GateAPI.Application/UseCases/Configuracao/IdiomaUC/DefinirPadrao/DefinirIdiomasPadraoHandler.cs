using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.DefinirPadrao
{
    public class DefinirIdiomasPadraoHandler(IIdiomaRepository idiomaRepository) : IRequestHandler<DefinirIdiomasPadraoCommand, Result<string>>
    {
        private readonly IIdiomaRepository _idiomaRepository = idiomaRepository;

        public async Task<Result<string>> Handle(DefinirIdiomasPadraoCommand command, CancellationToken cancellationToken = default)
        {
            // Buscar o idioma padrão atual
            var idiomaPadraoAtual = await _idiomaRepository.GetPadraoAsync(cancellationToken);

            // Se existe um idioma padrão, remover a marcação
            if (idiomaPadraoAtual != null)
            {
                idiomaPadraoAtual.RemoverComoPadrao();
                await _idiomaRepository.UpdateAsync(idiomaPadraoAtual, cancellationToken);
            }

            // Buscar o novo idioma que será padrão
            var novoIdiomasPadrao = await _idiomaRepository.GetByIdAsync(command.Id, cancellationToken);

            if (novoIdiomasPadrao == null)
                return Result<string>.Failure("Idioma não encontrado");

            try
            {
                novoIdiomasPadrao.DefinirComoPadrao();
                await _idiomaRepository.UpdateAsync(novoIdiomasPadrao, cancellationToken);

                return Result<string>.Success("Idioma definido como padrão com sucesso");
            }
            catch (ArgumentException ex)
            {
                return Result<string>.Failure(ex.Message);
            }
        }
    }
}
