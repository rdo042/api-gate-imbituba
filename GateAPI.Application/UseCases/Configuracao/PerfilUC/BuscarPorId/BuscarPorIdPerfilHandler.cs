using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.PerfilUC.BuscarPorId
{
    public class BuscarPorIdPerfilHandler(IPerfilRepository perfil) : ICommandHandler<BuscarPorIdPerfilQuery, Result<Perfil>>
    {
        private readonly IPerfilRepository _perfilRepository = perfil;

        public async Task<Result<Perfil>> HandleAsync(BuscarPorIdPerfilQuery command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _perfilRepository.GetByIdAsync(command.Id)
                    ?? throw new NullReferenceException("Nao encontrado pelo id " + command.Id);

                return Result<Perfil>.Success(result);

            }
            catch (NullReferenceException ex)
            {
                return Result<Perfil>.Failure("Erro ao buscar perfil - " + ex.Message);
            }
        }
    }
}
