using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;

namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.AlterarStatus
{
    public class AlterarStatusUsuarioHandler(IUsuarioRepository usuario) : ICommandHandler<AlterarStatusUsuarioCommand, Result<Guid>>
    {
        private readonly IUsuarioRepository _usuarioRepository = usuario;

        public async Task<Result<Guid>> HandleAsync(AlterarStatusUsuarioCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var existente = await _usuarioRepository.GetByIdAsync(command.Id)
                    ?? throw new NullReferenceException("Usuario nao encontrado pelo id" + command.Id);

                existente.UpdateStatus(command.Status);

                await _usuarioRepository.UpdateAsync(existente);

                return Result<Guid>.Success(command.Id);
            }
            catch (NullReferenceException ex)
            {
                return Result<Guid>.Failure("Erro ao atualizar status usuario - " + ex.Message);
            }
        }
    }
}
