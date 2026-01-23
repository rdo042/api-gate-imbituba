using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.BuscarTodos
{
    public class BuscarTodosLocalAvariaHandler(ILocalAvariaRepository localAvaria) : IRequestHandler<BuscarTodosLocalAvariaQuery, Result<IEnumerable<LocalAvaria>>>
    {
        private readonly ILocalAvariaRepository _localAvariaRepository = localAvaria;

        public async Task<Result<IEnumerable<LocalAvaria>>> Handle(BuscarTodosLocalAvariaQuery command, CancellationToken cancellationToken = default)
        {
            var locaisAvaria = await _localAvariaRepository.GetAllAsync();

            if (!locaisAvaria.Any())
                return Result<IEnumerable<LocalAvaria>>.Failure("Erro ao buscar locais de avaria");

            return Result<IEnumerable<LocalAvaria>>.Success(locaisAvaria);
        }
    }
}
