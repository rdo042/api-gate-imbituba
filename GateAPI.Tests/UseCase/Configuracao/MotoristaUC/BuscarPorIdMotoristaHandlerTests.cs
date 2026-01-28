using GateAPI.Application.UseCases.Configuracao.Base;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Tests.Entities.Configuracao.Stubs;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.MotoristaUC
{
    public class BuscarPorIdMotoristaHandlerTests
    {
        private readonly Mock<IMotoristaRepository> _repositoryMock;
        private readonly BuscarPorIdHandler<Motorista> _handler;

        public BuscarPorIdMotoristaHandlerTests()
        {
            _repositoryMock = new Mock<IMotoristaRepository>();
            _handler = new BuscarPorIdHandler<Motorista>(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoMotoristaForEncontrado()
        {
            // Arrange
            var motorista = MotoristaStub.Valid();
            var query = new BuscarPorIdQuery<Motorista>(motorista.Id);

            _repositoryMock.Setup(r => r.GetByIdAsync(motorista.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(motorista);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("João da Silva", result.Data.Nome);
            _repositoryMock.Verify(r => r.GetByIdAsync(motorista.Id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoMotoristaNoaoExiste()
        {
            // Arrange
            var motoristaId = Guid.NewGuid();
            var query = new BuscarPorIdQuery<Motorista>(motoristaId);

            _repositoryMock.Setup(r => r.GetByIdAsync(motoristaId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Motorista?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("não encontrado", result.Error);
            _repositoryMock.Verify(r => r.GetByIdAsync(motoristaId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}