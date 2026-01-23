using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Infra.Mappers.Configuracao;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using GateAPI.Infra.Persistence.Repositories.Configuracao;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Tests.Repositories.Configuracao
{
    public class TipoAvariaRepositoryTests
    {
        private static readonly TipoAvariaMapper mapper = new();
        private static AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var accessor = new HttpContextAccessor();
            return new AppDbContext(options, accessor);
        }

        private static readonly TipoAvariaModel _testSeed = new()
        {
            Id = Guid.NewGuid(),
            Tipo = "AVA001",
            Descricao = "",
            Status = StatusEnum.ATIVO
        };
        private static readonly TipoAvariaModel _testSeed2 = new()
        {
            Id = Guid.NewGuid(),
            Tipo = "AVA002",
            Descricao = "",
            Status = StatusEnum.ATIVO
        };

        private static void SeedContext(AppDbContext context)
        {
            context.TipoAvaria.Add(_testSeed);
            context.TipoAvaria.Add(_testSeed2);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetById_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TipoAvariaRepository(context, mapper);

            SeedContext(context);

            // Act
            var resultado = await repository.GetByIdAsync(_testSeed.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("LAC001", resultado.Tipo);
        }

        [Fact]
        public async Task GetById_DeveRetornarNull_QuandoNaoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TipoAvariaRepository(context, mapper);

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
            var repository = new TipoAvariaRepository(context, mapper);

            var entidade = new TipoAvaria(
                "AVA001",
                "",
                StatusEnum.ATIVO
                );

            // Act
            var resultado = await repository.AddAsync(entidade);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("AVA001", resultado.Tipo);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarTipoAvaria_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            SeedContext(context);
            context.ChangeTracker.Clear();
            var repository = new TipoAvariaRepository(context, mapper);

            var toUpdate = await context.TipoAvaria
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == _testSeed2.Id);

            if (toUpdate == null)
                return;

            var newDesc = "Teste";
            toUpdate.Descricao = newDesc;

            // Act
            var entidade = mapper.ToDomain(toUpdate);
            await repository.UpdateAsync(entidade);

            // Assert
            context.ChangeTracker.Clear();
            var updatedModel = await context.TipoAvaria.FindAsync(_testSeed2.Id);

            Assert.NotNull(updatedModel);
            Assert.Equal(newDesc, updatedModel.Descricao);
            Assert.Equal(_testSeed2.Id, updatedModel.Id);
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverTipoAvaria_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            SeedContext(context);
            var repository = new TipoAvariaRepository(context, mapper);
            var idToDelete = _testSeed.Id;

            // Act
            await repository.DeleteAsync(idToDelete);

            // Assert
            context.ChangeTracker.Clear();
            var notFoundDeleted = await context.TipoAvaria.FindAsync(idToDelete);
            Assert.Null(notFoundDeleted);

            var deletedModel = await context.TipoAvaria.IgnoreQueryFilters().FirstAsync(x => x.Id == idToDelete);
            Assert.NotNull(deletedModel);
            Assert.False(deletedModel.DeletedAt == null);

        }
    }
}
