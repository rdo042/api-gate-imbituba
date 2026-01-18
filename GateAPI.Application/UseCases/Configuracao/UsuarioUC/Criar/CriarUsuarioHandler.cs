using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Domain.Services;

namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.Criar
{
    public class CriarUsuarioHandler(
        IUsuarioRepository usuario,
        IPerfilRepository perfilRepository,
        IPasswordHasher passwordHasher) : ICommandHandler<CriarUsuarioCommand, Result<Usuario>>
    {
        private readonly IUsuarioRepository _usuarioRepository = usuario;
        private readonly IPerfilRepository _perfilRepository = perfilRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<Result<Usuario>> HandleAsync(CriarUsuarioCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var senhaHash = _passwordHasher.HashPassword(command.Senha);

                var perfil = await _perfilRepository.GetByIdAsync(command.PerfilId);

                var obj = new Usuario(command.Nome, command.Email, senhaHash, perfil);
                var result = await _usuarioRepository.AddAsync(obj);

                return Result<Usuario>.Success(result);

            }
            catch (Exception ex)
            {
                return Result<Usuario>.Failure("Erro ao criar usuario - " + ex.Message);
            }
        }
    }
}
