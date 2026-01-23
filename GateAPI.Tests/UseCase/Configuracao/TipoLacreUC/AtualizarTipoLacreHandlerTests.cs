using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TipoLacreUC
{
    public class AtualizarTipoLacreHandlerTests
    {
        private readonly Mock<ITipoLacreRepository> _repositoryMock;
        private readonly AtualizarTipoLacreHandler _handler;
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        public AtualizarTipoLacreHandlerTests()
        {
            _repositoryMock = new Mock<ITipoLacreRepository>();
            _handler = new AtualizarTipoLacreHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoTipoLacreForAtualizado()
        {
            // Arrange
            var existente = new TipoLacre("LAC001", "Lacre", _validStatusEnum);

            var command = new AtualizarTipoLacreCommand(
                existente.Id,
                "LAC002",
                "",
                _validStatusEnum
            );

            _repositoryMock.Setup(r => r.GetByIdAsync(existente.Id))
                           .ReturnsAsync(existente);

            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<TipoLacre>()))
                           .Returns(Task.CompletedTask);

            // Act: Executamos o Handler
            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            _repositoryMock.Verify(r => r.UpdateAsync(It.Is<TipoLacre>(n =>
                n.Tipo == "LAC002" &&
                n.Descricao == "" &&
                n.Status == _validStatusEnum
            )), Times.Once);
        }
    }
}
