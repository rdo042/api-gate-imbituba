using GateAPI.Application.UseCases.Configuracao.TaskFlowUC.Criar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TaskFlowUC
{
    public class CriarTaskFlowHandlerTests
    {
        private readonly Mock<ITaskFlowRepository> _repositoryMock;
        private readonly CriarTaskFlowHandler _handler;
        public CriarTaskFlowHandlerTests()
        {
            _repositoryMock = new Mock<ITaskFlowRepository>();
            _handler = new CriarTaskFlowHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso()
        {
            var nome = "Teste";

            var command = new CriarTaskFlowCommand(nome);

            var entidade = new TaskFlow(nome);

            _repositoryMock.Setup(r => r.AddAsync(entidade))
                    .ReturnsAsync(entidade);

            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<TaskFlow>(), CancellationToken.None), Times.Once);
        }
    }
}
