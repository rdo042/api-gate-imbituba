using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Criar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.LocalAvariaUC
{
    public class CriarLocalAvariaHandlerTests
    {
        private readonly Mock<ILocalAvariaRepository> _repositoryMock;
        private readonly CriarLocalAvariaHandler _handler;
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        public CriarLocalAvariaHandlerTests()
        {
            _repositoryMock = new Mock<ILocalAvariaRepository>();
            _handler = new CriarLocalAvariaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoLocalAvariaForCriado()
        {
            // Arrange
            var command = new CriarLocalAvariaCommand(
                "LOCAL001",
                "Descrição do local de avaria",
                _validStatusEnum
            );

            var criado = new LocalAvaria(command.Local, command.Descricao, command.Status);

            _repositoryMock.Setup(r => r.GetByLocalAsync(It.IsAny<string>()))
                           .ReturnsAsync((LocalAvaria?)null);

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<LocalAvaria>()))
                           .ReturnsAsync(criado);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("LOCAL001", result.Data.Local);
            _repositoryMock.Verify(r => r.GetByLocalAsync(command.Local), Times.Once);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<LocalAvaria>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoLocalJaExiste()
        {
            // Arrange
            var command = new CriarLocalAvariaCommand(
                "LOCAL001",
                "Descrição do local de avaria",
                _validStatusEnum
            );

            var localExistente = new LocalAvaria(command.Local, command.Descricao, command.Status);

            _repositoryMock.Setup(r => r.GetByLocalAsync(It.IsAny<string>()))
                           .ReturnsAsync(localExistente);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Já existe um Local Avaria", result.Error);
            _repositoryMock.Verify(r => r.GetByLocalAsync(command.Local), Times.Once);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<LocalAvaria>()), Times.Never);
        }

        [Theory]
        [InlineData("")] // Local vazio
        [InlineData("   ")] // Local apenas com espaços
        public async Task Handle_DeveFalhar_QuandoLocalEstaVazio(string local)
        {
            // Arrange
            var command = new CriarLocalAvariaCommand(local, "Descrição válida", _validStatusEnum);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command));
            
            Assert.NotNull(ex);
            Assert.Contains("Local", ex.Message);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<LocalAvaria>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoLocalEhMuitoLongo()
        {
            // Arrange - Criar uma string com mais de 100 caracteres
            var localLongo = new string('A', 101);
            var command = new CriarLocalAvariaCommand(localLongo, "Descrição válida", _validStatusEnum);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command));
            
            Assert.NotNull(ex);
            Assert.Contains("tamanho", ex.Message.ToLower());
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<LocalAvaria>()), Times.Never);
        }

        [Theory]
        [InlineData("")] // Descricao vazio
        [InlineData("   ")] // Descricao apenas com espaços
        public async Task Handle_DeveFalhar_QuandoDescricaoEstaVazia(string descricao)
        {
            // Arrange
            var command = new CriarLocalAvariaCommand("LOCAL001", descricao, _validStatusEnum);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command));
            
            Assert.NotNull(ex);
            Assert.Contains("Descrição", ex.Message);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<LocalAvaria>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoDescricaoEhMuitoLonga()
        {
            // Arrange - Criar uma string com mais de 255 caracteres
            var descricaoLonga = new string('A', 256);
            var command = new CriarLocalAvariaCommand("LOCAL001", descricaoLonga, _validStatusEnum);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command));
            
            Assert.NotNull(ex);
            Assert.Contains("tamanho", ex.Message.ToLower());
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<LocalAvaria>()), Times.Never);
        }
    }
}
