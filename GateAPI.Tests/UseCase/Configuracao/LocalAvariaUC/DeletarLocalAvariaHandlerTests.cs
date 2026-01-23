using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Deletar;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.LocalAvariaUC
{
    public class DeletarLocalAvariaHandlerTests
    {
        private readonly Mock<ILocalAvariaRepository> _repositoryMock;
        private readonly DeletarLocalAvariaHandler _handler;

        public DeletarLocalAvariaHandlerTests()
        {
            _repositoryMock = new Mock<ILocalAvariaRepository>();
            _handler = new DeletarLocalAvariaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoLocalAvariaForDeletado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeletarLocalAvariaCommand(id);

            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Guid>(), CancellationToken.None))
                           .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Value);
            _repositoryMock.Verify(r => r.DeleteAsync(id, CancellationToken.None), Times.Once);
        }
    }
}
