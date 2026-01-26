using GateAPI.Application.UseCases.Configuracao.IdiomaUC.Criar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.IdiomaUC
{
    public class CriarIdiomaHandlerTests
    {
        private readonly Mock<IIdiomaRepository> _repositoryMock;
        private readonly CriarIdiomaHandler _handler;

        public CriarIdiomaHandlerTests()
        {
            _repositoryMock = new Mock<IIdiomaRepository>();
            _handler = new CriarIdiomaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoIdiomaForCriado()
        {
            // Arrange
            var command = new CriarIdiomaCommand(
                "pt-BR",
                "Português Brasil",
                "Idioma português do Brasil",
                StatusEnum.ATIVO,
                CanalEnum.Ambos,
                true
            );

            var idiomaCriado = new Idioma(
                command.Codigo,
                command.Nome,
                command.Descricao,
                command.Status,
                command.Canal,
                command.EhPadrao
            );

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(idiomaCriado);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("pt-BR", result.Data!.Codigo);
            Assert.Equal("Português Brasil", result.Data.Nome);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoIdiomaForCriadoInativo()
        {
            // Arrange
            var command = new CriarIdiomaCommand(
                "en-US",
                "English USA",
                "English from USA",
                StatusEnum.INATIVO,
                CanalEnum.App,
                false
            );

            var idiomaCriado = new Idioma(
                command.Codigo,
                command.Nome,
                command.Descricao,
                command.Status,
                command.Canal,
                command.EhPadrao
            );

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(idiomaCriado);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(StatusEnum.INATIVO, result.Data.Status);
            Assert.False(result.Data.EhPadrao);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoTentarCriarSegundoIdiomaComoPadrao()
        {
            // Arrange
            var command = new CriarIdiomaCommand(
                "en-US",
                "English USA",
                "English from USA",
                StatusEnum.ATIVO,
                CanalEnum.App,
                true // Tentando criar como padrão
            );

            var idiomaPadraoExistente = new Idioma(
                "pt-BR",
                "Português Brasil",
                "Idioma português do Brasil",
                StatusEnum.ATIVO,
                CanalEnum.Ambos,
                true
            );

            _repositoryMock.Setup(r => r.GetPadraoAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(idiomaPadraoExistente);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Já existe um idioma padrão. Remova o padrão do idioma atual antes de definir um novo.", result.Error);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoCriarIdiomaComoPadraoSemOutroPadrao()
        {
            // Arrange
            var command = new CriarIdiomaCommand(
                "pt-BR",
                "Português Brasil",
                "Idioma português do Brasil",
                StatusEnum.ATIVO,
                CanalEnum.Ambos,
                true // Criar como padrão
            );

            var idiomaCriado = new Idioma(
                command.Codigo,
                command.Nome,
                command.Descricao,
                command.Status,
                command.Canal,
                command.EhPadrao
            );

            // Nenhum idioma padrão existe
            _repositoryMock.Setup(r => r.GetPadraoAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Idioma?)null);

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(idiomaCriado);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Data!.EhPadrao);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

