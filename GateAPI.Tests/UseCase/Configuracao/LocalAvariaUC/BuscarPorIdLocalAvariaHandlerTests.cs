using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.BuscarPorId;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.LocalAvariaUC
{
    public class BuscarPorIdLocalAvariaHandlerTests
    {
        private readonly Mock<ILocalAvariaRepository> _repositoryMock;
        private readonly BuscarPorIdLocalAvariaHandler _handler;
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        public BuscarPorIdLocalAvariaHandlerTests()
        {
            _repositoryMock = new Mock<ILocalAvariaRepository>();
            _handler = new BuscarPorIdLocalAvariaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoLocalAvariaForEncontrado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new BuscarPorIdLocalAvariaQuery(id);

            var localAvaria = new LocalAvaria("LOCAL001", "Descrição do local", _validStatusEnum);

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync(localAvaria);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("LOCAL001", result.Data.Local);
            Assert.Equal("Descrição do local", result.Data.Descricao);
            _repositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoNaoEncontrado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new BuscarPorIdLocalAvariaQuery(id);

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync((LocalAvaria?)null);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("não encontrado", result.Error.ToLower());
            _repositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        }
    }
}
