using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Domain.Services;

namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.Atualizar
{
    public class AtualizarUsuarioHandler(
        IUsuarioRepository usuario,
        IPerfilRepository perfilRepository,
        IPasswordHasher passwordHasher) : ICommandHandler<AtualizarUsuarioCommand, Result<object?>>
    {
        private readonly IUsuarioRepository _usuarioRepository = usuario;
        private readonly IPerfilRepository _perfilRepository = perfilRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<Result<object?>> HandleAsync(AtualizarUsuarioCommand command, CancellationToken cancellationToken = default)
        {
            var existente = await _usuarioRepository.GetByIdAsync(command.Id);

            if (existente == null)
                return Result<object?>.Failure("Usuário não encontrado pelo id - " + command.Id);

            var senhaHash = _passwordHasher.HashPassword(command.Senha);

            var perfil = await _perfilRepository.GetByIdAsync(command.PerfilId);

            existente.UpdateEntity(command.Nome, command.Email, senhaHash, command.Foto, perfil, command.Status);

            await _usuarioRepository.UpdateAsync(existente);

            return Result<object?>.Success(null);
        }
    }
}
