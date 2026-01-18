using GateAPI.Application.UseCases.Configuracao.PerfilUC.BuscarPorId;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.PerfilUC
{
    public class BuscarPorIdPerfilHandlerTests
    {
        private readonly Mock<IPerfilRepository> _repositoryMock;
        private readonly BuscarPorIdPerfilHandler _handler;

        public BuscarPorIdPerfilHandlerTests()
        {
            _repositoryMock = new Mock<IPerfilRepository>();
            _handler = new BuscarPorIdPerfilHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoPerfilForEncontrado()
        {
            // Arrange
            var existente = new Perfil("Perfil", "", []);

            var command = new BuscarPorIdPerfilQuery(existente.Id);

            _repositoryMock.Setup(r => r.GetByIdAsync(existente.Id))
                           .ReturnsAsync(existente);

            // Act: Executamos o Handler
            var result = await _handler.HandleAsync(command);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("Perfil", result.Data.Nome);
            _repositoryMock.Verify(r => r.GetByIdAsync(existente.Id), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_DeveFalhar_SeDadosForemInvalidos()
        {
            // Arrange
            var command = new BuscarPorIdPerfilQuery(Guid.Empty);

            // Act
            var result = await _handler.HandleAsync(command);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
