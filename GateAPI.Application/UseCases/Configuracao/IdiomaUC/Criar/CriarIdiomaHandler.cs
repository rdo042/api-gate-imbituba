using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.Criar
{
    public class CriarIdiomaHandler(IIdiomaRepository idiomaRepository) : IRequestHandler<CriarIdiomaCommand, Result<Idioma>>
    {
        private readonly IIdiomaRepository _idiomaRepository = idiomaRepository;

        public async Task<Result<Idioma>> Handle(CriarIdiomaCommand command, CancellationToken cancellationToken = default)
        {
            var idioma = new Idioma(
                command.Codigo,
                command.Nome,
                command.Descricao,
                command.Status,
                command.Canal,
                command.EhPadrao
            );

            var result = await _idiomaRepository.AddAsync(idioma, cancellationToken);

            return Result<Idioma>.Success(result);
        }
    }
}
