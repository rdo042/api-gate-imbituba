using FluentValidation;
using GateAPI.Application.UseCases.Configuracao.MotoristaUC.Criar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Tests.Entities.Configuracao.Stubs;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.MotoristaUC
{
    public class CriarMotoristaCommandHandlerTests
    {
        private readonly Mock<IMotoristaRepository> _repositoryMock;
        private readonly CriarMotoristaCommandHandler _handler;
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        public CriarMotoristaCommandHandlerTests()
        {
            _repositoryMock = new Mock<IMotoristaRepository>();
            _handler = new CriarMotoristaCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoMotoristaForCriado()
        {
            // Arrange
            var stub = MotoristaStub.Valid();
            var command = new CriarMotoristaCommand(
                "João da Silva",
                new DateOnly(1985, 5, 20),
                stub.RG.Value,
                stub.CPF.Value,
                stub.CNH.Value,
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                _validStatusEnum
            );

            var motoristaCriado = MotoristaStub.Valid();

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Motorista>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(motoristaCriado);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("João da Silva", result.Data.Nome);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Motorista>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        
        [Fact]
        public async Task Handle_DeveFalhar_QuandoCpfEInvalido()
        {
            // Arrange
            var command = new CriarMotoristaCommand(
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "000", // CPF com menos de 11 caracteres
                "12345678900",
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                _validStatusEnum
            );

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.NotNull(ex);
            Assert.Contains("CPF", ex.Message);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Motorista>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}