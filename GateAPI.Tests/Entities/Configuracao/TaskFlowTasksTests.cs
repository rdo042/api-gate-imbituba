using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Tests.Entities.Configuracao
{
    public class TaskFlowTasksTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateTaskFlow()
        {
            // Arrange
            var taskFlow = new TaskFlow("Teste");
            var task = Tasks.Load(Guid.NewGuid(), "Task1", "url", Domain.Enums.StatusEnum.ATIVO);

            // Act
            var entity = new TaskFlowTasks(
                taskFlow, task, 1, Domain.Enums.StatusEnum.ATIVO
            );

            // Assert
            Assert.Equal(taskFlow, entity.TaskFlow);
            Assert.Equal(task, entity.Tasks);
            Assert.Equal(1, entity.Ordem);
            Assert.Equal(Domain.Enums.StatusEnum.ATIVO, entity.Status);
        }

        [Fact]
        public void AlterarOrdem_DeveAlterarOrdemDoTaskFlowTasks()
        {
            // Arrange
            var taskFlow = new TaskFlow("Teste");
            var task = Tasks.Load(Guid.NewGuid(), "Task1", "url", Domain.Enums.StatusEnum.ATIVO);

            // Act
            var entity = new TaskFlowTasks(
                taskFlow, task, 1, Domain.Enums.StatusEnum.ATIVO
            );

            entity.AlterarOrdem(2);

            // Assert
            Assert.Equal(2, entity.Ordem);
        }
    }
}
