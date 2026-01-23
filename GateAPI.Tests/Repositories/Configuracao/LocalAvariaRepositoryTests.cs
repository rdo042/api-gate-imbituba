using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Infra.Mappers;
using GateAPI.Infra.Mappers.Configuracao;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using GateAPI.Infra.Persistence.Repositories.Configuracao;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Tests.Repositories.Configuracao
{
    public class LocalAvariaRepositoryTests
    {
        private static AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var accessor = new HttpContextAccessor();
            return new AppDbContext(options, accessor);
        }

        private static (LocalAvariaModel, LocalAvariaModel) CriarSeedPadrao()
        {
            var testSeed = new LocalAvariaModel()
            {
                Id = Guid.NewGuid(),
                Local = "LOCAL001",
                Descricao = "Descrição do local de avaria",
                Status = StatusEnum.ATIVO
            };
            var testSeed2 = new LocalAvariaModel()
            {
                Id = Guid.NewGuid(),
                Local = "LOCAL002",
                Descricao = "Descrição do segundo local",
                Status = StatusEnum.ATIVO
            };

            return (testSeed, testSeed2);
        }

        private static void SeedContext(AppDbContext context, LocalAvariaModel testSeed, LocalAvariaModel testSeed2)
        {
            context.LocalAvaria.Add(testSeed);
            context.LocalAvaria.Add(testSeed2);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetById_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new LocalAvariaMapper();
            var repository = new LocalAvariaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetByIdAsync(testSeed.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("LOCAL001", resultado.Local);
        }

        [Fact]
        public async Task GetById_DeveRetornarNull_QuandoNaoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var mapper = new LocalAvariaMapper();
            var repository = new LocalAvariaRepository(context, mapper);

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
            var mapper = new LocalAvariaMapper();
            var repository = new LocalAvariaRepository(context, mapper);

            var entidade = new LocalAvaria(
                "LOCAL001",
                "Descrição do local de avaria",
                StatusEnum.ATIVO
                );

            // Act
            var resultado = await repository.AddAsync(entidade);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("LOCAL001", resultado.Local);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarLocalAvaria_QuandoEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            SeedContext(context, testSeed, testSeed2);
            context.ChangeTracker.Clear();
            var mapper = new LocalAvariaMapper();
            var repository = new LocalAvariaRepository(context, mapper);

            var toUpdate = await context.LocalAvaria
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == testSeed2.Id);

            if (toUpdate == null)
                return;

            var newDescricao = "Nova descrição atualizada";
            toUpdate.Descricao = newDescricao;

            // Act
            var entidade = mapper.ToDomain(toUpdate);
            await repository.UpdateAsync(entidade);

            // Assert
            context.ChangeTracker.Clear();
            var updatedModel = await context.LocalAvaria.FindAsync(testSeed2.Id);

            Assert.NotNull(updatedModel);
            Assert.Equal(newDescricao, updatedModel.Descricao);
            Assert.Equal(testSeed2.Id, updatedModel.Id);
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverLocalAvaria_QuandoEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            SeedContext(context, testSeed, testSeed2);
            var mapper = new LocalAvariaMapper();
            var repository = new LocalAvariaRepository(context, mapper);
            var idToDelete = testSeed.Id;

            // Act
            await repository.DeleteAsync(idToDelete);

            // Assert
            context.ChangeTracker.Clear();
            var notFoundDeleted = await context.LocalAvaria.FindAsync(idToDelete);
            Assert.Null(notFoundDeleted);

            var deletedModel = await context.LocalAvaria.IgnoreQueryFilters().FirstAsync(x => x.Id == idToDelete);
            Assert.NotNull(deletedModel);
            Assert.NotNull(deletedModel.DeletedAt);
        }

        [Fact]
        public async Task GetByLocalAsync_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new LocalAvariaMapper();
            var repository = new LocalAvariaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetByLocalAsync(testSeed.Local);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("LOCAL001", resultado.Local);
            Assert.Equal(testSeed.Id, resultado.Id);
        }

        [Fact]
        public async Task GetByLocalAsync_DeveRetornarNull_QuandoNaoEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new LocalAvariaMapper();
            var repository = new LocalAvariaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetByLocalAsync("LOCAL_INEXISTENTE");

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task GetByLocalAsync_DeveRetornarNull_QuandoDeletado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new LocalAvariaMapper();
            var repository = new LocalAvariaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            await repository.DeleteAsync(testSeed.Id);

            // Assert
            context.ChangeTracker.Clear();
            var resultado = await repository.GetByLocalAsync(testSeed.Local);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task GetAllAppAsync_DeveRetornarSucesso_QuandoLocaisAtivosForemEncontrados()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new LocalAvariaMapper();
            var repository = new LocalAvariaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetAllAppAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Contains(resultado, x => x.Local == "LOCAL001");
            Assert.Contains(resultado, x => x.Local == "LOCAL002");
        }

        [Fact]
        public async Task GetAllAppAsync_DeveRetornarListaVazia_QuandoNenhumAtivoEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new LocalAvariaMapper();
            var repository = new LocalAvariaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            await repository.DeleteAsync(testSeed.Id);
            await repository.DeleteAsync(testSeed2.Id);

            context.ChangeTracker.Clear();

            // Act
            var resultado = await repository.GetAllAppAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Empty(resultado);
        }
    }
}
