using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Tests.Entities.Configuracao
{
    public class TaskFlowTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateTaskFlow()
        {
            // Arrange
            var nome = "TASKFLOW1";

            // Act
            var entity = new TaskFlow(
                nome
            );

            // Assert
            Assert.Equal(nome, entity.Nome);
        }

        [Theory]
        [InlineData("")]
        public void Constructor_WithInvalidPerfil_ShouldThrowArgumentException(string nome)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new TaskFlow(
                nome
            ));
        }

        [Fact]
        public void Load_WithValidNewTaskFlow_ShouldLoadTaskFlow()
        {
            // Arrange
            var model = new TaskFlowModel()
            {
                Id = Guid.NewGuid(),
                Nome = "EXPORTACAO",
                TaskFlowTasks = []
            };

            // Act
            var entidade = TaskFlow.Load(
                model.Id,
                model.Nome);

            // Assert
            Assert.Equal(model.Id, entidade.Id);
            Assert.Equal(model.Nome, entidade.Nome);
        }
    }
}
