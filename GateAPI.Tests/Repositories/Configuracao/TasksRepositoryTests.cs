using GateAPI.Domain.Enums;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using GateAPI.Infra.Persistence.Repositories.Configuracao;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Tests.Repositories.Configuracao
{
    public class TasksRepositoryTests
    {
        private static AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var accessor = new HttpContextAccessor();
            return new AppDbContext(options, accessor);
        }

        private static readonly TasksModel _testSeed = new()
        {
            Id = Guid.NewGuid(),
            Nome = "TESTE",
            Url = "TESTE",
            Status = StatusEnum.ATIVO
        };

        private static void SeedContext(AppDbContext context)
        {
            context.Tasks.Add(_testSeed);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetAllPorParametroAsync_DeveRetornar()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TasksRepository(context);

            SeedContext(context);

            // Act
            var resultado = await repository.GetAllPorParametroAsync("TEST");

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado);
        }
    }
}
