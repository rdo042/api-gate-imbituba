using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Criar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TipoLacreUC
{
    public class CriarTipoLacreHandlerTests
    {
        private readonly Mock<ITipoLacreRepository> _repositoryMock;
        private readonly CriarTipoLacreHandler _handler;
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        public CriarTipoLacreHandlerTests()
        {
            _repositoryMock = new Mock<ITipoLacreRepository>();
            _handler = new CriarTipoLacreHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoTipoLacreForCriado()
        {
            // Arrange
            var command = new CriarTipoLacreCommand(
                "LAC001",
                "Lacre armador",
                _validStatusEnum
            );

            var criado = new TipoLacre(command.Tipo, command.Descricao, command.Status);

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<TipoLacre>()))
                           .ReturnsAsync(criado);

            // Act: Executamos o Handler
            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("LAC001", result.Data.Tipo);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<TipoLacre>()), Times.Once);
        }

        //[Theory]
        //[InlineData("")] // Nome vazio
        //public async Task HandleAsync_DeveFalhar_SeDadosForemInvalidos(string tipo)
        //{
        //    // Arrange
        //    var command = new CriarTipoLacreCommand(tipo, null, _validStatusEnum);

        //    // Act
        //    var result = await _handler.Handle(command);

        //    // Assert
        //    Assert.Throws<ArgumentException>(() => result);
        //}
    }
}
