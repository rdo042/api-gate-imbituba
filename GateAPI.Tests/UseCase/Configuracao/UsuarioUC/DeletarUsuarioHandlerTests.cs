using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
using GateAPI.Application.UseCases.Configuracao.UsuarioUC.Deletar;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.UsuarioUC
{
    public class DeletarUsuarioHandlerTests
    {
        private readonly Mock<IUsuarioRepository> _repositoryMock;
        private readonly DeletarUsuarioHandler _handler;
        public DeletarUsuarioHandlerTests()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
            _handler = new DeletarUsuarioHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoUsuarioForDeletado()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var command = new DeletarUsuarioCommand(guid);

            _repositoryMock.Setup(r => r.DeleteAsync(guid))
                           .Returns(Task.CompletedTask);

            // Act: Executamos o Handler
            var result = await _handler.HandleAsync(command);

            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(r => r.DeleteAsync(guid), Times.Once);
        }
    }
}
