using GateAPI.Application.UseCases.Configuracao.IdiomaUC.BuscarTodos;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.IdiomaUC
{
    public class BuscarTodosIdiomaHandlerTests
    {
        private readonly Mock<IIdiomaRepository> _repositoryMock;
        private readonly BuscarTodosIdiomaHandler _handler;

        public BuscarTodosIdiomaHandlerTests()
        {
            _repositoryMock = new Mock<IIdiomaRepository>();
            _handler = new BuscarTodosIdiomaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarListaDeIdiomas()
        {
            // Arrange
            var query = new BuscarTodosIdiomaQuery();

            var idiomas = new List<Idioma>
            {
                new Idioma("pt-BR", "Português Brasil", "Idioma português", StatusEnum.ATIVO, CanalEnum.Ambos, true),
                new Idioma("en-US", "English USA", "English from USA", StatusEnum.ATIVO, CanalEnum.App, false),
                new Idioma("fr-FR", "Francês", "Idioma francês", StatusEnum.INATIVO, CanalEnum.Retaguarda, false)
            };

            _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(idiomas);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(3, result.Data.Count());
            _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarListaVazia_QuandoNaoHouverIdiomas()
        {
            // Arrange
            var query = new BuscarTodosIdiomaQuery();

            _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
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
