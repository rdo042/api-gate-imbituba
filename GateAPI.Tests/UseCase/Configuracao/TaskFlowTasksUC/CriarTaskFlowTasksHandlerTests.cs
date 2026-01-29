using GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.Criar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TaskFlowTasksUC
{
    public class CriarTaskFlowTasksHandlerTests
    {
        private readonly Mock<ITaskFlowTasksRepository> _relacaoMock;
        private readonly Mock<ITaskFlowRepository> _flowMock;
        private readonly Mock<ITasksRepository> _taskMock;
        private readonly CriarTaskFlowTasksHandler _handler;
        public CriarTaskFlowTasksHandlerTests()
        {
            _relacaoMock = new Mock<ITaskFlowTasksRepository>();
            _flowMock = new Mock<ITaskFlowRepository>();
            _taskMock = new Mock<ITasksRepository>();
            _handler = new CriarTaskFlowTasksHandler(_relacaoMock.Object, _flowMock.Object, _taskMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso()
        {
            var taskFlow = new TaskFlow("Teste");
            var task = Tasks.Load(Guid.NewGuid(), "Task1", "url", Domain.Enums.StatusEnum.ATIVO);

            var entidade = new TaskFlowTasks(
                taskFlow, task, 1, Domain.Enums.StatusEnum.ATIVO
            );

            var command = new CriarTaskFlowTasksCommand(taskFlow.Id, task.Id);

            _flowMock.Setup(r => r.GetByIdAsync(taskFlow.Id))
                    .ReturnsAsync(taskFlow);
            _taskMock.Setup(r => r.GetByIdAsync(task.Id, CancellationToken.None))
                    .ReturnsAsync(task);
            _relacaoMock.Setup(r => r.AddAsync(entidade))
                    .ReturnsAsync(entidade);

            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            _relacaoMock.Verify(r => r.AddAsync(It.IsAny<TaskFlowTasks>()), Times.Once);
        }
    }
}
