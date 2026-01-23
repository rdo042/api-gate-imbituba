using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Deletar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TipoAvariaUC
{
    public class DeletarTipoAvariaHandlerTests
    {
        private readonly Mock<ITipoAvariaRepository> _repositoryMock;
        private readonly DeletarTipoAvariaCommandHandler _handler;

        public DeletarTipoAvariaHandlerTests()
        {
            _repositoryMock = new Mock<ITipoAvariaRepository>();
            _handler = new DeletarTipoAvariaCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoTipoAvariaForDeletado()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var command = new DeletarTipoAvariaCommand(guid);

            _repositoryMock.Setup(r => r.DeleteAsync(guid, CancellationToken.None))
                           .ReturnsAsync(true);

            // Act: Executamos o Handler
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(r => r.DeleteAsync(guid, CancellationToken.None), Times.Once);
        }
    }
}
