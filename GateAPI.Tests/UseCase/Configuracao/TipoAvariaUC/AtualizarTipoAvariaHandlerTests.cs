using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TipoAvariaUC
{
    public class AtualizarTipoAvariaHandlerTests
    {
        private readonly Mock<ITipoAvariaRepository> _repositoryMock;
        private readonly AtualizarTipoAvariaCommandHandler _handler;
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        public AtualizarTipoAvariaHandlerTests()
        {
            _repositoryMock = new Mock<ITipoAvariaRepository>();
            _handler = new AtualizarTipoAvariaCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoTipoAvariaForAtualizado()
        {
            // Arrange
            var existente = new TipoAvaria("TpAvaria", "Avaria descrição", _validStatusEnum);

            var command = new AtualizarTipoAvariaCommand(
                existente.Id,
                "TpAvaria",
                "",
                _validStatusEnum
            );

            _repositoryMock.Setup(r => r.GetByIdAsync(existente.Id, CancellationToken.None))
                           .ReturnsAsync(existente);

            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<TipoAvaria>(), CancellationToken.None))
                           .Returns(Task.CompletedTask);

            // Act: Executamos o Handler
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(existente.Id, result.Data);
            _repositoryMock.Verify(r => r.UpdateAsync(It.Is<TipoAvaria>(n =>
                n.Tipo == "TpAvaria" &&
                n.Descricao == "" &&
                n.Status == _validStatusEnum
            ), CancellationToken.None), Times.Once);
        }
    }
}
