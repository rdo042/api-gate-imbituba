using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.BuscarPorId
{
    public class BuscarPorIdIdiomaHandler(IIdiomaRepository idiomaRepository) : IRequestHandler<BuscarPorIdIdiomaQuery, Result<Idioma>>
    {
        private readonly IIdiomaRepository _idiomaRepository = idiomaRepository;

        public async Task<Result<Idioma>> Handle(BuscarPorIdIdiomaQuery query, CancellationToken cancellationToken = default)
        {
            var idioma = await _idiomaRepository.GetByIdAsync(query.Id, cancellationToken);

            if (idioma == null)
                return Result<Idioma>.Failure("Idioma não encontrado");

            return Result<Idioma>.Success(idioma);
        }
    }
}
