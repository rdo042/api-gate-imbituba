using GateAPI.Application.UseCases.Configuracao.MotoristaUC.BuscarTodos;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Tests.Entities.Configuracao.Stubs;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.MotoristaUC
{
    public class BuscarPorDocumentoMotoristaHandlerTests
    {
        private readonly Mock<IMotoristaRepository> _repositoryMock;
        private readonly BuscarPorDocumentosMotoristaHandler _handler;

        public BuscarPorDocumentoMotoristaHandlerTests()
        {
            _repositoryMock = new Mock<IMotoristaRepository>();
            _handler = new BuscarPorDocumentosMotoristaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoMotoristaForEncontradoPorDocumento()
        {
            // Arrange
            var motorista = MotoristaStub.Valid();
            var query = new BuscarPorDocumentoMotoristaQuery(motorista.CPF.Value);

            _repositoryMock.Setup(r => r.GetByDocumentoAsync(motorista.CPF.Value, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(motorista);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("João da Silva", result.Data.Nome);
            _repositoryMock.Verify(r => r.GetByDocumentoAsync(motorista.CPF.Value, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoDocumentoNoaoExiste()
        {
            // Arrange
            var documento = "12345678901";
            var query = new BuscarPorDocumentoMotoristaQuery(documento);

            _repositoryMock.Setup(r => r.GetByDocumentoAsync(documento, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Domain.Entities.Configuracao.Motorista?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("não encontrado", result.Error);
            _repositoryMock.Verify(r => r.GetByDocumentoAsync(documento, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData("")] // Documento vazio
        [InlineData("   ")] // Documento com apenas espaços
        public async Task Handle_DeveFalhar_QuandoDocumentoEInvalido(string documento)
        {
            // Arrange
            var query = new BuscarPorDocumentoMotoristaQuery(documento);

            _repositoryMock.Setup(r => r.GetByDocumentoAsync(documento, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Domain.Entities.Configuracao.Motorista?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            _repositoryMock.Verify(r => r.GetByDocumentoAsync(documento, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}