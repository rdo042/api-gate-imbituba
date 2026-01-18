using GateAPI.Application.UseCases.Configuracao.UsuarioUC.Atualizar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Domain.Services;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.UsuarioUC
{
    public class AtualizarUsuarioHandlerTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;
        private readonly Mock<IPerfilRepository> _perfilMock;
        private readonly Mock<IPasswordHasher> _hasherMock;
        private readonly AtualizarUsuarioHandler _handler;
        public AtualizarUsuarioHandlerTests()
        {
            _repoMock = new Mock<IUsuarioRepository>();
            _perfilMock = new Mock<IPerfilRepository>();
            _hasherMock = new Mock<IPasswordHasher>();
            _handler = new AtualizarUsuarioHandler(_repoMock.Object, _perfilMock.Object, _hasherMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoTipoLacreForAtualizado()
        {
            // Arrange
            var existente = new Usuario("John", "john@email.com", "hash_valido", null);

            var command = new AtualizarUsuarioCommand(
                existente.Id,
                "Jane",
                "jane@email.com",
                "hash_valido",
                Guid.Empty,
                Domain.Enums.StatusEnum.ATIVO
            );

            _repoMock.Setup(r => r.GetByIdAsync(existente.Id))
                           .ReturnsAsync(existente);

            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Usuario>()))
                           .Returns(Task.CompletedTask);

            _hasherMock.Setup(h => h.HashPassword(It.IsAny<string>()))
                .Returns("hash_valido");

            // Act: Executamos o Handler
            var result = await _handler.HandleAsync(command);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("Jane", result.Data.Nome);
            Assert.Equal("jane@email.com", result.Data.Email);
            _repoMock.Verify(r => r.UpdateAsync(It.Is<Usuario>(n =>
                n.Nome == "Jane" &&
                n.Email == "jane@email.com"
            )), Times.Once);
        }
    }
}
