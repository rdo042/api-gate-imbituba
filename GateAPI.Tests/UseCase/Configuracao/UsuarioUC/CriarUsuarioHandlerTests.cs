using GateAPI.Application.UseCases.Configuracao.UsuarioUC.Criar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Domain.Services;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.UsuarioUC
{
    public class CriarUsuarioHandlerTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;
        private readonly Mock<IPerfilRepository> _perfilMock;
        private readonly Mock<IPasswordHasher> _hasherMock;
        private readonly CriarUsuarioHandler _handler;
        public CriarUsuarioHandlerTests()
        {
            _repoMock = new Mock<IUsuarioRepository>();
            _perfilMock = new Mock<IPerfilRepository>();
            _hasherMock = new Mock<IPasswordHasher>();
            _handler = new CriarUsuarioHandler(_repoMock.Object, _perfilMock.Object, _hasherMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoUsuarioForCriado()
        {
            // Arrange
            var command = new CriarUsuarioCommand(
                "Teste",
                "teste@email.com", 
                "hash_valido",
                Guid.Empty
            );

            var criado = new Usuario(command.Nome, command.Email, command.Senha, null);

            _repoMock.Setup(r => r.AddAsync(It.IsAny<Usuario>()))
                           .ReturnsAsync(criado);

            _hasherMock.Setup(h=> h.HashPassword(It.IsAny<string>()))
                .Returns("hash_valido");

            // Act: Executamos o Handler
            var result = await _handler.HandleAsync(command);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Usuario>()), Times.Once);
        }
    }
}
