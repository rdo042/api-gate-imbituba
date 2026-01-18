using GateAPI.Application.UseCases.Configuracao.UsuarioUC.BuscarPorId;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Services;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.UsuarioUC
{
    public class BuscarPorIdUsuarioHandlerTests
    {
        private readonly Mock<IUsuarioRepository> _repositoryMock;
        private readonly BuscarPorIdUsuarioHandler _handler;
        private readonly PasswordHasher _hasher;
        public BuscarPorIdUsuarioHandlerTests()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
            _handler = new BuscarPorIdUsuarioHandler(_repositoryMock.Object);
            _hasher = new PasswordHasher();
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoUsuarioForEncontrado()
        {
            // Arrange
            var nome = "John Doe";
            var email = "johndoe@email.com";
            var senha = "#Senha123";
            var senhaHash = _hasher.HashPassword(senha);

            var existente = new Usuario(
                nome, email, senhaHash, null
            );

            var command = new BuscarPorIdUsuarioQuery(existente.Id);

            _repositoryMock.Setup(r => r.GetByIdAsync(existente.Id))
                           .ReturnsAsync(existente);

            // Act: Executamos o Handler
            var result = await _handler.HandleAsync(command);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            _repositoryMock.Verify(r => r.GetByIdAsync(existente.Id), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_DeveFalhar_SeDadosForemInvalidos()
        {
            // Arrange
            var command = new BuscarPorIdUsuarioQuery(Guid.Empty);

            // Act
            var result = await _handler.HandleAsync(command);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
