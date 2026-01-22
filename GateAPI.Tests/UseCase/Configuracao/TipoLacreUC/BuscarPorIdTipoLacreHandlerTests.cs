using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.BuscarPorId;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TipoLacreUC
{
    public class BuscarPorIdTipoLacreHandlerTests
    {
        private readonly Mock<ITipoLacreRepository> _repositoryMock;
        private readonly BuscarPorIdTipoLacreHandler _handler;

        public BuscarPorIdTipoLacreHandlerTests()
        {
            _repositoryMock = new Mock<ITipoLacreRepository>();
            _handler = new BuscarPorIdTipoLacreHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoTipoLacreForEncontrado()
        {
            // Arrange
            var existente = new TipoLacre("LAC001", "", StatusEnum.ATIVO);

            var command = new BuscarPorIdTipoLacreQuery(existente.Id);

            _repositoryMock.Setup(r => r.GetByIdAsync(existente.Id))
                           .ReturnsAsync(existente);

            // Act: Executamos o Handler
            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("LAC001", result.Data.Tipo);
            _repositoryMock.Verify(r => r.GetByIdAsync(existente.Id), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_DeveFalhar_SeDadosForemInvalidos()
        {
            // Arrange
            var command = new BuscarPorIdTipoLacreQuery(Guid.Empty);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
