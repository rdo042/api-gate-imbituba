using GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.AlterarOrdem;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TaskFlowTasksUC
{
    public class AlterarOrdemTaskFlowTasksHandlerTests
    {
        private readonly Mock<ITaskFlowTasksRepository> _relacaoMock;
        private readonly AlterarOrdemTaskFlowTasksHandler _handler;
        public AlterarOrdemTaskFlowTasksHandlerTests()
        {
            _relacaoMock = new Mock<ITaskFlowTasksRepository>();
            _handler = new AlterarOrdemTaskFlowTasksHandler(_relacaoMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso()
        {
            var taskFlow = new TaskFlow("Teste");
            var task = Tasks.Load(Guid.NewGuid(), "Task1", "url", Domain.Enums.StatusEnum.ATIVO);
            var task2 = Tasks.Load(Guid.NewGuid(), "Task2", "url", Domain.Enums.StatusEnum.ATIVO);

            var entidade = new TaskFlowTasks(
                taskFlow, task, 1, Domain.Enums.StatusEnum.ATIVO
            );
            var entidade2 = new TaskFlowTasks(
                taskFlow, task2, 2, Domain.Enums.StatusEnum.ATIVO
            );

            var command = new AlterarOrdemTaskFlowTasksCommand(taskFlow.Id, task.Id, false);

            _relacaoMock.Setup(r => r.GetSpecificAsync(taskFlow.Id, task.Id))
                    .ReturnsAsync(entidade);

            _relacaoMock.Setup(r => r.GetByOrderAsync(taskFlow.Id, 2))
                    .ReturnsAsync(entidade2);

            IEnumerable<TaskFlowTasks> lista = [entidade, entidade2];

            _relacaoMock.Setup(r => r.UpdateRange(lista))
                    .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            _relacaoMock.Verify(r => r.UpdateRange(lista), Times.Once);
        }
    }
}
