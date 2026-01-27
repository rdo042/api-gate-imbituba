using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Deletar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.MotoristaUC
{
    public class DeletarMotoristaCommandHandlerTests
    {
        private readonly Mock<IMotoristaRepository> _repositoryMock;
        private readonly DeletarMotoristaCommandHandler _handler;

        public DeletarMotoristaCommandHandlerTests()
        {
            _repositoryMock = new Mock<IMotoristaRepository>();
            _handler = new DeletarMotoristaCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoMotoristaForDeletado()
        {
            // Arrange
            var motoristaId = Guid.NewGuid();
            var command = new DeletarMotoristaCommand(motoristaId);

            _repositoryMock.Setup(r => r.DeleteAsync(motoristaId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(r => r.DeleteAsync(motoristaId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoMotoristaNaoExiste()
        {
            // Arrange
            var motoristaId = Guid.NewGuid();
            var command = new DeletarMotoristaCommand(motoristaId);

            _repositoryMock.Setup(r => r.DeleteAsync(motoristaId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            _repositoryMock.Verify(r => r.DeleteAsync(motoristaId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}