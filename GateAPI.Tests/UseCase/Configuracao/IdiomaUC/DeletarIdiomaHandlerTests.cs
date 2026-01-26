using GateAPI.Application.UseCases.Configuracao.IdiomaUC.Deletar;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.IdiomaUC
{
    public class DeletarIdiomaHandlerTests
    {
        private readonly Mock<IIdiomaRepository> _repositoryMock;
        private readonly DeletarIdiomaHandler _handler;

        public DeletarIdiomaHandlerTests()
        {
            _repositoryMock = new Mock<IIdiomaRepository>();
            _handler = new DeletarIdiomaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoIdiomaForDeletado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeletarIdiomaCommand(id);

            _repositoryMock.Setup(r => r.DeleteAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("Idioma deletado com sucesso", result.Data);
            _repositoryMock.Verify(r => r.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoIdiomaForNaoEncontrado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeletarIdiomaCommand(id);

            _repositoryMock.Setup(r => r.DeleteAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Idioma não encontrado", result.Error);
        }
    }
}
