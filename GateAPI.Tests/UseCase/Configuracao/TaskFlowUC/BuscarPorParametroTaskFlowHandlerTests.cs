using GateAPI.Application.UseCases.Configuracao.TaskFlowUC.BuscarPorParametro;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TaskFlowUC
{
    public class BuscarPorParametroTaskFlowHandlerTests
    {
        private readonly Mock<ITaskFlowRepository> _repositoryMock;
        private readonly Mock<ITaskFlowTasksRepository> _repositoryMock2;
        private readonly BuscarPorParametroTaskFlowHandler _handler;
        public BuscarPorParametroTaskFlowHandlerTests()
        {
            _repositoryMock = new Mock<ITaskFlowRepository>();
            _repositoryMock2 = new Mock<ITaskFlowTasksRepository>();
            _handler = new BuscarPorParametroTaskFlowHandler(_repositoryMock.Object, _repositoryMock2.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso()
        {
            // Arrange
            var entidade = new TaskFlow("Teste");

            var task = Tasks.Load(Guid.NewGuid(), "Tarefa de Teste", "url", Domain.Enums.StatusEnum.ATIVO);

            var lista = new List<TaskFlowTasks>
            {
                new(entidade, task, 1, Domain.Enums.StatusEnum.ATIVO)
            };

            var command = new BuscarPorParametroTaskFlowQuery(entidade.Id);

            _repositoryMock.Setup(r => r.GetByIdAsync(command.Id))
                    .ReturnsAsync(entidade);

            _repositoryMock2.Setup(r => r.GetByFlowIdAsync(command.Id))
                    .ReturnsAsync(lista);

            // Act: Executamos o Handler
            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data.Tasks);
            _repositoryMock.Verify(r => r.GetByIdAsync(command.Id), Times.Once);
        }
    }
}
