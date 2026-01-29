using GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.AlterarStatus;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TaskFlowTasksUC
{
    public class AlterarStatusTaskFlowTasksHandlerTests
    {
        private readonly Mock<ITaskFlowTasksRepository> _relacaoMock;
        private readonly AlterarStatusTaskFlowTasksHandler _handler;
        public AlterarStatusTaskFlowTasksHandlerTests()
        {
            _relacaoMock = new Mock<ITaskFlowTasksRepository>();
            _handler = new AlterarStatusTaskFlowTasksHandler(_relacaoMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso()
        {
            var taskFlow = new TaskFlow("Teste");
            var task = Tasks.Load(Guid.NewGuid(), "Task1", "url", Domain.Enums.StatusEnum.ATIVO);

            var entidade = new TaskFlowTasks(
                taskFlow, task, 1, Domain.Enums.StatusEnum.ATIVO
            );

            var command = new AlterarStatusTaskFlowTasksCommand(taskFlow.Id, task.Id, Domain.Enums.StatusEnum.INATIVO);

            _relacaoMock.Setup(r => r.GetSpecificAsync(taskFlow.Id, task.Id))
                    .ReturnsAsync(entidade);

            IEnumerable<TaskFlowTasks> lista = [entidade];

            _relacaoMock.Setup(r => r.UpdateRange(lista))
                    .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            _relacaoMock.Verify(r => r.UpdateRange(lista), Times.Once);
        }
    }
}
