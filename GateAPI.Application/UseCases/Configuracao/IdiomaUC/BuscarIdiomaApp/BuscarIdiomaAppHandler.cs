using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.IdiomaUC.BuscarIdiomaApp
{
    public class BuscarIdiomaAppHandler(IIdiomaRepository idiomaRepository) : IRequestHandler<BuscarIdiomaAppQuery, Result<IEnumerable<Idioma>>>
    {
        private readonly IIdiomaRepository _idiomaRepository = idiomaRepository;

        public async Task<Result<IEnumerable<Idioma>>> Handle(BuscarIdiomaAppQuery query, CancellationToken cancellationToken = default)
        {
            var idiomas = await _idiomaRepository.GetByCanalAsync((int)CanalEnum.App, cancellationToken);

            return Result<IEnumerable<Idioma>>.Success(idiomas);
        }
    }
}
