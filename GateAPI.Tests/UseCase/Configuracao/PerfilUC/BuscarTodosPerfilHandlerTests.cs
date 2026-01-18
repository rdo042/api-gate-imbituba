using GateAPI.Application.UseCases.Configuracao.PerfilUC.BuscarTodos;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.PerfilUC
{
    public class BuscarTodosPerfilHandlerTests
    {
        private readonly Mock<IPerfilRepository> _repositoryMock;
        private readonly BuscarTodosPerfilHandler _handler;

        public BuscarTodosPerfilHandlerTests()
        {
            _repositoryMock = new Mock<IPerfilRepository>();
            _handler = new BuscarTodosPerfilHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso()
        {
            // Arrange
            var existente = new Perfil("Perfil", "", []);

            var command = new BuscarTodosPerfilQuery();

            _repositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync([existente]);

            // Act: Executamos o Handler
            var result = await _handler.HandleAsync(command);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
