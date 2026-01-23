using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TipoAvariaUC
{
    public class BuscarPorIdTipoAvariaHandlerTests
    {
        private readonly Mock<ITipoAvariaRepository> _repositoryMock;
        private readonly BuscarPorIdTipoAvariaHandler _handler;

        public BuscarPorIdTipoAvariaHandlerTests()
        {
            _repositoryMock = new Mock<ITipoAvariaRepository>();
            _handler = new BuscarPorIdTipoAvariaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoTipoAvariaForEncontrado()
        {
            // Arrange
            var existente = new TipoAvaria("AVA001", "", StatusEnum.ATIVO);

            var command = new BuscarPorIdTipoAvariaQuery(existente.Id);

            _repositoryMock.Setup(r => r.GetByIdAsync(existente.Id, CancellationToken.None))
                           .ReturnsAsync(existente);

            // Act: Executamos o Handler
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("AVA001", result.Data.Tipo);
            _repositoryMock.Verify(r => r.GetByIdAsync(existente.Id, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_DeveFalhar_SeDadosForemInvalidos()
        {
            // Arrange
            var command = new BuscarPorIdTipoAvariaQuery(Guid.Empty);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
