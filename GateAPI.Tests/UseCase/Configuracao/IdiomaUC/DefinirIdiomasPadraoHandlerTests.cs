using GateAPI.Application.UseCases.Configuracao.IdiomaUC.DefinirPadrao;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.IdiomaUC
{
    public class DefinirIdiomasPadraoHandlerTests
    {
        private readonly Mock<IIdiomaRepository> _repositoryMock;
        private readonly DefinirIdiomasPadraoHandler _handler;

        public DefinirIdiomasPadraoHandlerTests()
        {
            _repositoryMock = new Mock<IIdiomaRepository>();
            _handler = new DefinirIdiomasPadraoHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoIdiomaForDefinidoComoPadrao()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DefinirIdiomasPadraoCommand(id);

            var idiomaPadraoAtual = new Idioma("pt-BR", "Português Brasil", "Idioma português", StatusEnum.ATIVO, CanalEnum.Ambos, true);
            var novoIdiomaPadrao = new Idioma("en-US", "English USA", "English from USA", StatusEnum.ATIVO, CanalEnum.App, false);

            _repositoryMock.Setup(r => r.GetPadraoAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(idiomaPadraoAtual);

            _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(novoIdiomaPadrao);

            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("Idioma definido como padrão com sucesso", result.Data);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoNovoIdiomaForNaoEncontrado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DefinirIdiomasPadraoCommand(id);

            var idiomaPadraoAtual = new Idioma("pt-BR", "Português Brasil", "Idioma português", StatusEnum.ATIVO, CanalEnum.Ambos, true);

            _repositoryMock.Setup(r => r.GetPadraoAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(idiomaPadraoAtual);

            _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Idioma?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Idioma não encontrado", result.Error);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoTentarDefinirIdiomaInativoComoPadrao()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DefinirIdiomasPadraoCommand(id);

            var idiomaPadraoAtual = new Idioma("pt-BR", "Português Brasil", "Idioma português", StatusEnum.ATIVO, CanalEnum.Ambos, true);
            var idiomaInativo = new Idioma("en-US", "English USA", "English from USA", StatusEnum.INATIVO, CanalEnum.App, false);

            _repositoryMock.Setup(r => r.GetPadraoAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(idiomaPadraoAtual);

            _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(idiomaInativo);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Um idioma inativo não pode ser definido como padrão.", result.Error);
        }

        [Fact]
        public async Task Handle_DeveDefinirComoPadrao_QuandoNaoHouverIdiomaAtual()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DefinirIdiomasPadraoCommand(id);

            var novoIdiomaPadrao = new Idioma("pt-BR", "Português Brasil", "Idioma português", StatusEnum.ATIVO, CanalEnum.Ambos, false);

            _repositoryMock.Setup(r => r.GetPadraoAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Idioma?)null);

            _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(novoIdiomaPadrao);

            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("Idioma definido como padrão com sucesso", result.Data);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
