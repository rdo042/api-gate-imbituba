using GateAPI.Application.UseCases.Configuracao.PerfilUC.Atualizar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.PerfilUC
{
    public class AtualizarPerfilHandlerTests
    {
        private readonly Mock<IPerfilRepository> _repositoryMock;
        private readonly AtualizarPerfilHandler _handler;

        public AtualizarPerfilHandlerTests()
        {
            _repositoryMock = new Mock<IPerfilRepository>();
            _handler = new AtualizarPerfilHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoPerfilForAtualizado()
        {
            // Arrange
            var existente = new Perfil("ADMIN", "", []);

            var command = new AtualizarPerfilCommand(
                existente.Id,
                "OPERACIONAL",
                "",
                Domain.Enums.StatusEnum.ATIVO,
                []
            );

            _repositoryMock.Setup(r => r.GetByIdAsync(existente.Id))
                           .ReturnsAsync(existente);

            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Perfil>()))
                           .Returns(Task.CompletedTask);

            // Act: Executamos o Handler
            var result = await _handler.HandleAsync(command);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("OPERACIONAL", result.Data.Nome);
            _repositoryMock.Verify(r => r.UpdateAsync(It.Is<Perfil>(n =>
                n.Nome == "OPERACIONAL" &&
                n.Descricao == ""
            )), Times.Once);
        }
    }
}
