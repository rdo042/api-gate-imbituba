using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Mappers.Configuracao;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using GateAPI.Infra.Persistence.Repositories.Configuracao;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Tests.Repositories.Configuracao
{
    public class TaskFlowTasksRepositoryTests
    {
        private static AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var accessor = new HttpContextAccessor();
            return new AppDbContext(options, accessor);
        }

        private static readonly TaskFlowTasksMapper mapper = new();

        private static readonly TaskFlowModel _seedTaskFlow = new()
        {
            Id = Guid.NewGuid(),
            Nome = "Teste",
            TaskFlowTasks = []
        };

        private static readonly TasksModel _seedTasks = new()
        {
            Id = Guid.NewGuid(),
            Nome = "Tarefa Teste",
            Url = "http://teste.com",
            Status = Domain.Enums.StatusEnum.ATIVO
        };
        private static readonly TasksModel _seedTasks2 = new()
        {
            Id = Guid.NewGuid(),
            Nome = "Tarefa Teste2",
            Url = "http://teste2.com",
            Status = Domain.Enums.StatusEnum.ATIVO
        };

        private static readonly TaskFlowTasksModel _testSeed = new()
        {
            Id = Guid.NewGuid(),
            TaskFlow = _seedTaskFlow,
            Tasks = _seedTasks,
            Ordem = 1,
            Status = Domain.Enums.StatusEnum.ATIVO
        };

        private static readonly TaskFlowTasksModel _testSeed2 = new()
        {
            Id = Guid.NewGuid(),
            TaskFlow = _seedTaskFlow,
            Tasks = _seedTasks2,
            Ordem = 2,
            Status = Domain.Enums.StatusEnum.ATIVO
        };

        private static void SeedContext(AppDbContext context)
        {
            context.TaskFlow.Add(_seedTaskFlow);

            context.Tasks.Add(_seedTasks);
            context.Tasks.Add(_seedTasks2);

            context.TaskFlowTasks.Add(_testSeed);
            context.TaskFlowTasks.Add(_testSeed2);
            context.SaveChanges();
        }

        [Fact]
        public async Task AddAsync_DeveAdicionar()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TaskFlowTasksRepository(context, mapper);

            var entidade = new TaskFlowTasks(
                new TaskFlow(_seedTaskFlow.Nome),
                Tasks.Load(_seedTasks.Id, _seedTasks.Nome, _seedTasks.Url, _seedTasks.Status),
                1,
                Domain.Enums.StatusEnum.ATIVO
                );

            // Act
            var resultado = await repository.AddAsync(entidade);

            // Assert
            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task GetByOrderAsync_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TaskFlowTasksRepository(context, mapper);

            SeedContext(context);

            // Act
            var resultado = await repository.GetByOrderAsync(_testSeed.TaskFlowId, _testSeed.Ordem);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Tarefa Teste", resultado.Tasks.Nome);
        }

        [Fact]
        public async Task GetSpecificAsync_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TaskFlowTasksRepository(context, mapper);

            SeedContext(context);

            // Act
            var resultado = await repository.GetSpecificAsync(_testSeed.TaskFlowId, _testSeed.TasksId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Tarefa Teste", resultado.Tasks.Nome);
        }

        [Fact]
        public async Task GetByFlowIdAsync_DeveRetornarLista()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TaskFlowTasksRepository(context, mapper);

            SeedContext(context);

            // Act
            var resultado = await repository.GetByFlowIdAsync(_testSeed.TaskFlowId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task GetMaxOrdemAsync_DeveRetornarTotalLista()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TaskFlowTasksRepository(context, mapper);

            SeedContext(context);

            // Act
            var resultado = await repository.GetMaxOrdemAsync(_testSeed.TaskFlowId);

            // Assert
            Assert.Equal(2, resultado);
        }

        [Fact]
        public async Task RemoveAndShiftAsync_DeveRemoverItemEReorganizarLista()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TaskFlowTasksRepository(context, mapper);

            SeedContext(context);

            // Act
            await repository.RemoveAndShiftAsync(_testSeed.TaskFlowId, _testSeed.TasksId);

            context.ChangeTracker.Clear();
            var resultado = await repository.GetByFlowIdAsync(_seedTaskFlow.Id);

            // Assert
            Assert.Single(resultado);
            Assert.Equal(1, resultado.FirstOrDefault()?.Ordem);
        }

        //[Fact]
        //public async Task Remove_DeveRetornarTrue_QuandoEncontrado()
        //{
        //    // Arrange
        //    using var context = CriarContextoInMemory();
        //    var repository = new TaskFlowTasksRepository(context, mapper);

        //    SeedContext(context);

        //    // Act
        //    var resultado = await repository.Remove(_testSeed.Id);

        //    // Assert
        //    Assert.True(resultado);
        //}
    }
}
