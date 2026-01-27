using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Tests.Entities.Configuracao.Stubs;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.MotoristaUC
{
    public class AtualizarMotoristaCommandHandlerTests
    {
        private readonly Mock<IMotoristaRepository> _repositoryMock;
        private readonly AtualizarMotoristaCommandHandler _handler;
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        public AtualizarMotoristaCommandHandlerTests()
        {
            _repositoryMock = new Mock<IMotoristaRepository>();
            _handler = new AtualizarMotoristaCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoMotoristaForAtualizado()
        {
            // Arrange
            var motoristaId = Guid.NewGuid();
            var motorista = MotoristaStub.Valid();

            var command = new AtualizarMotoristaCommand(
                motoristaId,
                "Jane Silva",
                new DateOnly(1990, 3, 15),
                motorista.RG.Value,
                motorista.CPF.Value,
                motorista.CNH.Value,
                new DateOnly(2027, 6, 30),
                "31988888888",
                null,
                _validStatusEnum
            );

            _repositoryMock.Setup(r => r.GetByIdAsync(motoristaId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(motorista);

            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Domain.Entities.Configuracao.Motorista>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(motoristaId, result.Data);
            _repositoryMock.Verify(r => r.GetByIdAsync(motoristaId, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Domain.Entities.Configuracao.Motorista>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoMotoristaNoaoExiste()
        {
            // Arrange
            var motoristaId = Guid.NewGuid();
            var command = new AtualizarMotoristaCommand(
                motoristaId,
                "Jane Silva",
                new DateOnly(1990, 3, 15),
                "987654321",
                "98765432109",
                "98765432101",
                new DateOnly(2027, 6, 30),
                "31988888888",
                null,
                _validStatusEnum
            );

            _repositoryMock.Setup(r => r.GetByIdAsync(motoristaId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Domain.Entities.Configuracao.Motorista?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("não encontrado", result.Error);
            _repositoryMock.Verify(r => r.GetByIdAsync(motoristaId, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Domain.Entities.Configuracao.Motorista>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}