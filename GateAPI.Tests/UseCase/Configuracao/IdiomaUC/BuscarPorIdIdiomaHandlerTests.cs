using GateAPI.Application.UseCases.Configuracao.IdiomaUC.BuscarPorId;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.IdiomaUC
{
    public class BuscarPorIdIdiomaHandlerTests
    {
        private readonly Mock<IIdiomaRepository> _repositoryMock;
        private readonly BuscarPorIdIdiomaHandler _handler;

        public BuscarPorIdIdiomaHandlerTests()
        {
            _repositoryMock = new Mock<IIdiomaRepository>();
            _handler = new BuscarPorIdIdiomaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarIdioma_QuandoEncontrado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new BuscarPorIdIdiomaQuery(id);

            var idioma = new Idioma("pt-BR", "Português Brasil", "Idioma português", StatusEnum.ATIVO, CanalEnum.Ambos, true);

            _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(idioma);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("pt-BR", result.Data.Codigo);
            Assert.True(result.Data.EhPadrao);
            _repositoryMock.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoIdiomaForNaoEncontrado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new BuscarPorIdIdiomaQuery(id);

            _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Idioma?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Idioma não encontrado", result.Error);
        }
    }
}
