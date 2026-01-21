using GateAPI.Application.UseCases.Configuracao.UsuarioUC.AlterarStatus;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.UsuarioUC
{
    public class AlterarStatusUsuarioHandlerTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;
        private readonly AlterarStatusUsuarioHandler _handler;
        public AlterarStatusUsuarioHandlerTests()
        {
            _repoMock = new Mock<IUsuarioRepository>();
            _handler = new AlterarStatusUsuarioHandler(_repoMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoTipoLacreForAtualizado()
        {
            // Arrange
            var existente = new Usuario("John", "john@email.com", "hash_valido", null, null);

            var command = new AlterarStatusUsuarioCommand(
                existente.Id,
                StatusEnum.INATIVO
            );

            _repoMock.Setup(r => r.GetByIdAsync(existente.Id))
                           .ReturnsAsync(existente);

            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Usuario>()))
                           .Returns(Task.CompletedTask);

            // Act: Executamos o Handler
            var result = await _handler.HandleAsync(command);

            Assert.True(result.IsSuccess);
            _repoMock.Verify(r => r.UpdateAsync(It.Is<Usuario>(n =>
                n.Status == StatusEnum.INATIVO
            )), Times.Once);
        }
    }
}
