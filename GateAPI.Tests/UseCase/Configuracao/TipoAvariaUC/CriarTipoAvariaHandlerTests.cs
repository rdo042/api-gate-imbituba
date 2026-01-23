using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TipoAvariaUC
{
    public class CriarTipoAvariaHandlerTests
    {
        private readonly Mock<ITipoAvariaRepository> _repositoryMock;
        private readonly CriarTipoAvariaCommandHandler _handler;
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        public CriarTipoAvariaHandlerTests()
        {
            _repositoryMock = new Mock<ITipoAvariaRepository>();
            _handler = new CriarTipoAvariaCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoTipoAvariaForCriado()
        {
            // Arrange
            var command = new CriarTipoAvariaCommand(
                "LAC001",
                "Lacre armador",
                _validStatusEnum
            );

            var criado = new TipoAvaria(command.Tipo, command.Descricao, command.Status);

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<TipoAvaria>(), CancellationToken.None))
                           .ReturnsAsync(criado);

            // Act: Executamos o Handler
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("LAC001", result.Data.Tipo);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<TipoAvaria>(), CancellationToken.None), Times.Once);
        }

        [Theory]
        [InlineData("")]
        public async Task HandleAsync_DeveFalhar_SeDadosForemInvalidos(string tipo)
        {
            // Arrange
            var command = new CriarTipoAvariaCommand(tipo, null, _validStatusEnum);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}
