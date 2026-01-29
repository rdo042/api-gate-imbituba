using GateAPI.Application.UseCases.Configuracao.TasksUC.BuscarTodosPorParametro;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TasksUC
{
    public class BuscarTodosPorParametroTasksHandlerTests
    {
        private readonly Mock<ITasksRepository> _repositoryMock;
        private readonly BuscarTodosPorParametroTasksHandler _handler;
        public BuscarTodosPorParametroTasksHandlerTests()
        {
            _repositoryMock = new Mock<ITasksRepository>();
            _handler = new BuscarTodosPorParametroTasksHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso()
        {
            // Arrange
            var nome = "Teste";
            
            var command = new BuscarTodosPorParametroTasksQuery(nome);

            _repositoryMock.Setup(r => r.GetAllPorParametroAsync(command.Nome))
                    .ReturnsAsync([]);

            // Act: Executamos o Handler
            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(r => r.GetAllPorParametroAsync(command.Nome), Times.Once);
        }
    }
}
