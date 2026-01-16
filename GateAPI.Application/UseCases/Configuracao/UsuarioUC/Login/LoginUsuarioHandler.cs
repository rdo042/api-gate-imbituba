using GateAPI.Application.Common.Interfaces.IrisApi.Application.Common.Interfaces;
using GateAPI.Application.Common.Models;
using GateAPI.Application.Providers;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Domain.Services;

namespace GateAPI.Application.UseCases.Configuracao.UsuarioUC.Login
{
    public record LoginUsuarioHandler(
        IUsuarioRepository UsuarioRepositorio,
        IPasswordHasher PasswordHasher,
        ITokenProvider TokenProvider
        ) : ICommandHandler<LoginUsuarioQuery, Result<string>>
    {
        private readonly IUsuarioRepository _usuarioRepository = UsuarioRepositorio;
        private readonly IPasswordHasher _hasher = PasswordHasher;
        private readonly ITokenProvider _tokenProvider = TokenProvider;

        public async Task<Result<string>> HandleAsync(LoginUsuarioQuery command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _usuarioRepository.GetByEmailAsync(command.Email)
                    ?? throw new Exception("Usuário ou senha inválidos");

                var isValid = _hasher.VerifyPassword(command.Senha, result.SenhaHash);
                if (!isValid) throw new Exception("Usuário ou senha inválidos");

                // 3. Gera o Token
                var token = _tokenProvider.GenerateToken(result)
                    ?? throw new Exception("Erro ao gerar token");

                return Result<string>.Success(token);

            }
            catch (Exception ex)
            {
                return Result<string>.Failure("Erro ao realizar login - " + ex.Message);
            }
        }
    }
}
