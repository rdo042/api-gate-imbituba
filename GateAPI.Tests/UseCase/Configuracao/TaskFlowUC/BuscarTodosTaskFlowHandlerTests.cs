using GateAPI.Application.UseCases.Configuracao.TaskFlowUC.BuscarTodos;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TaskFlowUC
{
    public class BuscarTodosTaskFlowHandlerTests
    {
        private readonly Mock<ITaskFlowRepository> _repositoryMock;
        private readonly BuscarTodosTaskFlowHandler _handler;
        public BuscarTodosTaskFlowHandlerTests()
        {
            _repositoryMock = new Mock<ITaskFlowRepository>();
            _handler = new BuscarTodosTaskFlowHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso()
        {
            var entidade = new TaskFlow("Teste");

            IEnumerable<TaskFlow> lista = [
                entidade
                ];

            var command = new BuscarTodosTaskFlowQuery();

            _repositoryMock.Setup(r => r.GetAllAsync())
                    .ReturnsAsync(lista);

            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
