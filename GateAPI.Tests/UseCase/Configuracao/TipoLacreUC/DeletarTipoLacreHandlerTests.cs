using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TipoLacreUC
{
    public class DeletarTipoLacreHandlerTests
    {
        private readonly Mock<ITipoLacreRepository> _repositoryMock;
        private readonly DeletarTipoLacreHandler _handler;

        public DeletarTipoLacreHandlerTests()
        {
            _repositoryMock = new Mock<ITipoLacreRepository>();
            _handler = new DeletarTipoLacreHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoTipoLacreForDeletado()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var command = new DeletarTipoLacreCommand(guid);

            _repositoryMock.Setup(r => r.DeleteAsync(guid))
                           .Returns(Task.CompletedTask);

            // Act: Executamos o Handler
            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(r => r.DeleteAsync(guid), Times.Once);
        }
    }
}
