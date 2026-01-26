using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.BuscarTodos
{
    public class BuscarTodosIdiomaHandler(IIdiomaRepository idiomaRepository) : IRequestHandler<BuscarTodosIdiomaQuery, Result<IEnumerable<Idioma>>>
    {
        private readonly IIdiomaRepository _idiomaRepository = idiomaRepository;

        public async Task<Result<IEnumerable<Idioma>>> Handle(BuscarTodosIdiomaQuery query, CancellationToken cancellationToken = default)
        {
            var idiomas = await _idiomaRepository.GetAllAsync(cancellationToken);

            return Result<IEnumerable<Idioma>>.Success(idiomas);
        }
    }
}
