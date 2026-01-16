using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Domain.Services;

namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.Atualizar
{
    public class AtualizarUsuarioHandler(
        IUsuarioRepository usuario,
        IPasswordHasher passwordHasher) : ICommandHandler<AtualizarUsuarioCommand, Result<Usuario>>
    {
        private readonly IUsuarioRepository _usuarioRepository = usuario;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<Result<Usuario>> HandleAsync(AtualizarUsuarioCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var existente = await _usuarioRepository.GetByIdAsync(command.Id)
                    ?? throw new NullReferenceException("Usuario nao encontrado pelo id" + command.Id);

                var senhaHash = _passwordHasher.HashPassword(command.Senha);

                //var perfil = _perfilRepository.GetById(command.PerfilId);

                existente.UpdateEntity(command.Nome, command.Email, senhaHash, null, command.Status);

                await _usuarioRepository.UpdateAsync(existente);

                return Result<Usuario>.Success(existente);

            }
            catch (Exception ex)
            {
                return Result<Usuario>.Failure("Erro ao atualizar usuario - " + ex.Message);
            }
        }
    }
}
