using GateAPI.Application.UseCases.Configuracao.IdiomaUC.BuscarIdiomaApp;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.IdiomaUC
{
    public class BuscarIdiomaAppHandlerTests
    {
        private readonly Mock<IIdiomaRepository> _repositoryMock;
        private readonly BuscarIdiomaAppHandler _handler;

        public BuscarIdiomaAppHandlerTests()
        {
            _repositoryMock = new Mock<IIdiomaRepository>();
            _handler = new BuscarIdiomaAppHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarIdiomasDoApp()
        {
            // Arrange
            var query = new BuscarIdiomaAppQuery();

            var idiomas = new List<Idioma>
            {
                new Idioma("pt-BR", "Português Brasil", "Idioma português", StatusEnum.ATIVO, CanalEnum.Ambos, true),
                new Idioma("en-US", "English USA", "English from USA", StatusEnum.ATIVO, CanalEnum.App, false)
            };

            _repositoryMock.Setup(r => r.GetByCanalAsync((int)CanalEnum.App, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(idiomas);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count());
            _repositoryMock.Verify(r => r.GetByCanalAsync((int)CanalEnum.App, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarListaVazia_QuandoNaoHouverIdiomasNoApp()
        {
            // Arrange
            var query = new BuscarIdiomaAppQuery();

            _repositoryMock.Setup(r => r.GetByCanalAsync((int)CanalEnum.App, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(new List<Idioma>());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
        }
    }
}
