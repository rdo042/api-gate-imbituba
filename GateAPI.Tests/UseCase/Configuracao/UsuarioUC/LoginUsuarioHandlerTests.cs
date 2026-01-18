using GateAPI.Application.Providers;
using GateAPI.Application.UseCases.Configuracao.UsuarioUC.Login;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Domain.Services;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.UsuarioUC
{
    public class LoginUsuarioHandlerTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;
        private readonly Mock<IPasswordHasher> _hasherMock;
        private readonly Mock<ITokenProvider> _tokenMock;
        private readonly LoginUsuarioHandler _handler;
        public LoginUsuarioHandlerTests()
        {
            _repoMock = new Mock<IUsuarioRepository>();
            _hasherMock = new Mock<IPasswordHasher>();
            _tokenMock = new Mock<ITokenProvider>();
            _handler = new LoginUsuarioHandler(_repoMock.Object, _hasherMock.Object, _tokenMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoCredenciaisForemValidas()
        {
            // Arrange
            var command = new LoginUsuarioQuery("teste@email.com", "senha123");
            var usuario = new Usuario("Teste", "teste@email.com", "hash_valido", null );
            var tokenEsperado = "jwt_token_gerado";

            _repoMock.Setup(x => x.GetByEmailAsync(command.Email))
                     .ReturnsAsync(usuario);

            _hasherMock.Setup(x => x.VerifyPassword(command.Senha, usuario.SenhaHash))
                       .Returns(true);

            _tokenMock.Setup(x => x.GenerateToken(usuario))
                      .Returns(tokenEsperado);

            // Act
            var result = await _handler.HandleAsync(command);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(tokenEsperado);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarFalha_QuandoSenhaForIncorreta()
        {
            // Arrange
            var command = new LoginUsuarioQuery("teste@email.com", "senha_errada");
            var usuario = new Usuario("Teste", "teste@email.com", "hash_valido", null);

            _repoMock.Setup(x => x.GetByEmailAsync(command.Email))
                     .ReturnsAsync(usuario);

            _hasherMock.Setup(x => x.VerifyPassword(command.Senha, usuario.SenhaHash))
                       .Returns(false);

            // Act
            var result = await _handler.HandleAsync(command);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Erro ao realizar login - Usuário ou senha inválidos", result.Error);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarFalha_QuandoUsuarioNaoExistir()
        {
            // Arrange
            var command = new LoginUsuarioQuery("nao_existe@email.com", "123");
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
            _repoMock.Setup(x => x.GetByEmailAsync(command.Email))
                     .ReturnsAsync((Usuario)null);
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.

            // Act
            var result = await _handler.HandleAsync(command);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Erro ao realizar login - Usuário ou senha inválidos", result.Error);
        }
    }
}
