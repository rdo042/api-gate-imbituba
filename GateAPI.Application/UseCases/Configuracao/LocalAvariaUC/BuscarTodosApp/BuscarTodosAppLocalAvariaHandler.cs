using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.BuscarTodosApp
{
    public class BuscarTodosAppLocalAvariaHandler(ILocalAvariaRepository localAvaria) : IRequestHandler<BuscarTodosAppLocalAvariaQuery, Result<IEnumerable<LocalAvaria>>>
    {
        private readonly ILocalAvariaRepository _localAvariaRepository = localAvaria;

        public async Task<Result<IEnumerable<LocalAvaria>>> Handle(BuscarTodosAppLocalAvariaQuery command, CancellationToken cancellationToken = default)
        {
            var locaisAvaria = await _localAvariaRepository.GetAllAppAsync();

            if (!locaisAvaria.Any())
                return Result<IEnumerable<LocalAvaria>>.Failure("Erro ao buscar locais de avaria");

            return Result<IEnumerable<LocalAvaria>>.Success(locaisAvaria);
        }
    }
}
