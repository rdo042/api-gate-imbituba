using GateAPI.Application.UseCases.Configuracao.IdiomaUC.Atualizar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.IdiomaUC
{
    public class AtualizarIdiomaHandlerTests
    {
        private readonly Mock<IIdiomaRepository> _repositoryMock;
        private readonly AtualizarIdiomaHandler _handler;

        public AtualizarIdiomaHandlerTests()
        {
            _repositoryMock = new Mock<IIdiomaRepository>();
            _handler = new AtualizarIdiomaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoIdiomaForAtualizado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new AtualizarIdiomaCommand(
                id,
                "pt-BR",
                "Português Brasil - Atualizado",
                "Idioma português do Brasil atualizado",
                StatusEnum.ATIVO,
                CanalEnum.Ambos
            );

            var idiomaExistente = new Idioma("pt-BR", "Português Brasil", "Descrição antiga", StatusEnum.ATIVO, CanalEnum.App, false);

            _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(idiomaExistente);

            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("Idioma atualizado com sucesso", result.Data);
            _repositoryMock.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoIdiomaForNaoEncontrado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new AtualizarIdiomaCommand(
                id,
                "fr-FR",
                "Francês",
                "Idioma francês",
                StatusEnum.ATIVO,
                CanalEnum.Retaguarda
            );

            _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Idioma?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Idioma não encontrado", result.Error);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Idioma>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
