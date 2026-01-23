using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Atualizar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.LocalAvariaUC
{
    public class AtualizarLocalAvariaHandlerTests
    {
        private readonly Mock<ILocalAvariaRepository> _repositoryMock;
        private readonly AtualizarLocalAvariaHandler _handler;
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        public AtualizarLocalAvariaHandlerTests()
        {
            _repositoryMock = new Mock<ILocalAvariaRepository>();
            _handler = new AtualizarLocalAvariaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoLocalAvariaForAtualizado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new AtualizarLocalAvariaCommand(
                id,
                "LOCAL001",
                "Descrição atualizada",
                _validStatusEnum
            );

            var localAvariaExistente = new LocalAvaria("LOCAL_ANTIGO", "Descrição antiga", _validStatusEnum);

            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                           .ReturnsAsync(localAvariaExistente);

            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<LocalAvaria>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("LOCAL001", result.Data.Local);
            _repositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<LocalAvaria>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoNaoEncontrado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new AtualizarLocalAvariaCommand(
                id,
                "LOCAL001",
                "Descrição",
                _validStatusEnum
            );

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync((LocalAvaria?)null);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("nao encontrado", result?.Error?.ToLower());
            _repositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<LocalAvaria>()), Times.Never);
        }

        [Theory]
        [InlineData("")] // Local vazio
        [InlineData("   ")] // Local apenas com espaços
        public async Task Handle_DeveFalhar_QuandoLocalEstaVazio(string local)
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new AtualizarLocalAvariaCommand(id, local, "Descrição válida", _validStatusEnum);

            var localAvariaExistente = new LocalAvaria("LOCAL_ANTIGO", "Descrição antiga", _validStatusEnum);

            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                           .ReturnsAsync(localAvariaExistente);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command));
            
            Assert.NotNull(ex);
            Assert.Contains("Local", ex.Message);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<LocalAvaria>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoLocalEhMuitoLongo()
        {
            // Arrange
            var id = Guid.NewGuid();
            var localLongo = new string('A', 101);
            var command = new AtualizarLocalAvariaCommand(id, localLongo, "Descrição válida", _validStatusEnum);

            var localAvariaExistente = new LocalAvaria("LOCAL_ANTIGO", "Descrição antiga", _validStatusEnum);

            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                           .ReturnsAsync(localAvariaExistente);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command));
            
            Assert.NotNull(ex);
            Assert.Contains("tamanho", ex.Message.ToLower());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<LocalAvaria>()), Times.Never);
        }

        [Theory]
        [InlineData("")] // Descricao vazio
        [InlineData("   ")] // Descricao apenas com espaços
        public async Task Handle_DeveFalhar_QuandoDescricaoEstaVazia(string descricao)
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new AtualizarLocalAvariaCommand(id, "LOCAL001", descricao, _validStatusEnum);

            var localAvariaExistente = new LocalAvaria("LOCAL_ANTIGO", "Descrição antiga", _validStatusEnum);

            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                           .ReturnsAsync(localAvariaExistente);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command));
            
            Assert.NotNull(ex);
            Assert.Contains("Descrição", ex.Message);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<LocalAvaria>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoDescricaoEhMuitoLonga()
        {
            // Arrange
            var id = Guid.NewGuid();
            var descricaoLonga = new string('A', 256);
            var command = new AtualizarLocalAvariaCommand(id, "LOCAL001", descricaoLonga, _validStatusEnum);

            var localAvariaExistente = new LocalAvaria("LOCAL_ANTIGO", "Descrição antiga", _validStatusEnum);

            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                           .ReturnsAsync(localAvariaExistente);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command));
            
            Assert.NotNull(ex);
            Assert.Contains("tamanho", ex.Message.ToLower());
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<LocalAvaria>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoLocalJaExiste()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new AtualizarLocalAvariaCommand(
                id,
                "LOCAL001",
                "Descrição atualizada",
                _validStatusEnum
            );

            var localAvariaExistente = new LocalAvaria("LOCAL_ANTIGO", "Descrição antiga", _validStatusEnum);

            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                           .ReturnsAsync(localAvariaExistente);

            _repositoryMock.Setup(r => r.GetByLocalAsync(command.Local))
                           .ReturnsAsync(new LocalAvaria("LOCAL001", "Descrição", _validStatusEnum));

            // Act
            var result = await _handler.Handle(command);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Já existe", result.Error);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<LocalAvaria>()), Times.Never);
        }
    }
}
