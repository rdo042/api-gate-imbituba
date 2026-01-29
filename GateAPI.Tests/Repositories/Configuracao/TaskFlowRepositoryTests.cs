using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Mappers.Configuracao;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using GateAPI.Infra.Persistence.Repositories.Configuracao;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Tests.Repositories.Configuracao
{
    public class TaskFlowRepositoryTests
    {
        private static AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var accessor = new HttpContextAccessor();
            return new AppDbContext(options, accessor);
        }
        private static readonly TaskFlowMapper mapper = new();

        private static readonly TaskFlowModel _testSeed = new()
        {
            Id = Guid.NewGuid(),
            Nome = "Teste",
            TaskFlowTasks = []
        };

        private static void SeedContext(AppDbContext context)
        {
            context.TaskFlow.Add(_testSeed);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetById_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TaskFlowRepository(context, mapper);

            SeedContext(context);

            // Act
            var resultado = await repository.GetByIdAsync(_testSeed.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Teste", resultado.Nome);
        }

        [Fact]
        public async Task GetById_DeveRetornarNull_QuandoNaoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TaskFlowRepository(context, mapper);

            // Act
            var resultado = await repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task AddAsync_DeveAdicionar()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TaskFlowRepository(context, mapper);

            var entidade = new TaskFlow(
                "Teste"
                );

            // Act
            var resultado = await repository.AddAsync(entidade);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Teste", resultado.Nome);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarTaskFlow_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            SeedContext(context);
            context.ChangeTracker.Clear();
            var repository = new TaskFlowRepository(context, mapper);

            var toUpdate = await context.TaskFlow
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == _testSeed.Id);

            if (toUpdate == null)
                return;

            var newNome = "Teste1";
            toUpdate.Nome = newNome;

            // Act
            var entidade = mapper.ToDomain(toUpdate);
            await repository.UpdateAsync(entidade);

            // Assert
            context.ChangeTracker.Clear();
            var updatedModel = await context.TaskFlow.FindAsync(_testSeed.Id);

            Assert.NotNull(updatedModel);
            Assert.Equal(newNome, updatedModel.Nome);
            Assert.Equal(_testSeed.Id, updatedModel.Id);
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverTipoLacre_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            SeedContext(context);
            var repository = new TaskFlowRepository(context, mapper);
            var idToDelete = _testSeed.Id;

            // Act
            await repository.DeleteAsync(idToDelete);

            // Assert
            context.ChangeTracker.Clear();
            var notFoundDeleted = await context.TaskFlow.FindAsync(idToDelete);
            Assert.Null(notFoundDeleted);

            var deletedModel = await context.TaskFlow.IgnoreQueryFilters().FirstAsync(x => x.Id == idToDelete);
            Assert.NotNull(deletedModel);
            Assert.NotNull(deletedModel.DeletedAt);
        }
    }
}
