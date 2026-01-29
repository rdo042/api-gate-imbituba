using GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.Criar;
using GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.Deletar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TaskFlowTasksUC
{
    public class DeletarTaskFlowTasksHandlerTests
    {
        private readonly Mock<ITaskFlowTasksRepository> _relacaoMock;
        private readonly DeletarTaskFlowTasksHandler _handler;
        public DeletarTaskFlowTasksHandlerTests()
        {
            _relacaoMock = new Mock<ITaskFlowTasksRepository>();
            _handler = new DeletarTaskFlowTasksHandler(_relacaoMock.Object);
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

            var command = new DeletarTaskFlowTasksCommand(taskFlow.Id, task.Id);

            _relacaoMock.Setup(r => r.RemoveAndShiftAsync(taskFlow.Id, task.Id))
                    .ReturnsAsync(true);

            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            _relacaoMock.Verify(r => r.RemoveAndShiftAsync(taskFlow.Id, task.Id), Times.Once);
        }
    }
}
