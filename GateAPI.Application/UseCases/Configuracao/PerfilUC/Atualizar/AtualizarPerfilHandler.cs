using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.PerfilUC.Atualizar
{
    public class AtualizarPerfilHandler(IPerfilRepository perfil) : ICommandHandler<AtualizarPerfilCommand, Result<Perfil>>
    {
        private readonly IPerfilRepository _perfilRepository = perfil;

        public async Task<Result<Perfil>> HandleAsync(AtualizarPerfilCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _perfilRepository.GetByIdAsync(command.Id)
                    ?? throw new NullReferenceException("Nao encontrado pelo id " + command.Id);

                var permissoes = command.Permissoes.Select(item => Permissao.Load(item.Id, item.Nome)).ToList();

                result.UpdateEntity(command.Nome, command.Descricao, result.Status, permissoes);
                await _perfilRepository.UpdateAsync(result);

                return Result<Perfil>.Success(result);

            }
            catch (Exception ex)
            {
                return Result<Perfil>.Failure("Erro ao atualizar Perfil - " + ex.Message);
            }
        }
    }
}
