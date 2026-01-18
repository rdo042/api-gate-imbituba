using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.PerfilUC.Deletar
{
    public class DeletarPerfilHandler(IPerfilRepository perfil) : ICommandHandler<DeletarPerfilCommand, Result<Guid>>
    {
        private readonly IPerfilRepository _perfilRepository = perfil;

        public async Task<Result<Guid>> HandleAsync(DeletarPerfilCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                await _perfilRepository.DeleteAsync(command.Id);

                return Result<Guid>.Success(command.Id);

            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure("Erro ao criar Perfil - " + ex.Message);
            }
        }
    }
}
