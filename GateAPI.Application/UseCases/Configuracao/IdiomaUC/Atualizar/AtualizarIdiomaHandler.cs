using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.Atualizar
{
    public class AtualizarIdiomaHandler(IIdiomaRepository idiomaRepository) : IRequestHandler<AtualizarIdiomaCommand, Result<string>>
    {
        private readonly IIdiomaRepository _idiomaRepository = idiomaRepository;

        public async Task<Result<string>> Handle(AtualizarIdiomaCommand command, CancellationToken cancellationToken = default)
        {
            var idioma = await _idiomaRepository.GetByIdAsync(command.Id, cancellationToken);

            if (idioma == null)
                return Result<string>.Failure("Idioma não encontrado");

            idioma.UpdateEntity(
                command.Codigo,
                command.Nome,
                command.Descricao,
                command.Status,
                command.Canal
            );

            await _idiomaRepository.UpdateAsync(idioma, cancellationToken);

            return Result<string>.Success("Idioma atualizado com sucesso");
        }
    }
}
