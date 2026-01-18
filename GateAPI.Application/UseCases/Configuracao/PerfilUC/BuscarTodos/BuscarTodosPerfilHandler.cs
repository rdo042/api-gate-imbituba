using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.PerfilUC.BuscarTodos
{
    public class BuscarTodosPerfilHandler(IPerfilRepository perfil) : ICommandHandler<BuscarTodosPerfilQuery, Result<IEnumerable<Perfil>>>
    {
        private readonly IPerfilRepository _perfilRepository = perfil;

        public async Task<Result<IEnumerable<Perfil>>> HandleAsync(BuscarTodosPerfilQuery command, CancellationToken cancellationToken = default)
        {
            var result = await _perfilRepository.GetAllAsync();

            if(result.Any())
                return Result<IEnumerable<Perfil>>.Success(result);

            return Result<IEnumerable<Perfil>>.Failure("Não encontrado nenhum perfil cadastrado");
        }
    }
}
