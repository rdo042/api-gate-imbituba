using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Tests.Entities.Configuracao
{
    public class TasksTests
    {
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        [Fact]
        public void Load_WithValidNewTasks_ShouldLoadTasks()
        {
            // Arrange
            var model = new TasksModel()
            {
                Id = Guid.NewGuid(),
                Nome = "EXPORTACAO",
                Url = "https",
                Status = _validStatusEnum
            };

            // Act
            var entidade = Tasks.Load(
                model.Id,
                model.Nome, 
                model.Url,
                model.Status);

            // Assert
            Assert.Equal(model.Id, entidade.Id);
            Assert.Equal(model.Nome, entidade.Nome);
            Assert.Equal(model.Url, entidade.Url);
            Assert.Equal(model.Status, entidade.Status);
        }
    }
}
