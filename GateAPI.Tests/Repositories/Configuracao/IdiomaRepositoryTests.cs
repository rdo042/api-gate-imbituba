using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Mappers;
using GateAPI.Infra.Mappers.Configuracao;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using GateAPI.Infra.Persistence.Repositories.Configuracao;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Tests.Repositories.Configuracao
{
    public class IdiomaRepositoryTests
    {
        private static AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var accessor = new HttpContextAccessor();
            return new AppDbContext(options, accessor);
        }

        private static (IdiomaModel, IdiomaModel) CriarSeedPadrao()
        {
            var testSeed = new IdiomaModel()
            {
                Id = Guid.NewGuid(),
                Codigo = "pt-BR",
                Nome = "Português Brasil",
                Descricao = "Idioma português do Brasil",
                Status = StatusEnum.ATIVO,
                Canal = 3, // Ambos
                EhPadrao = true
            };
            var testSeed2 = new IdiomaModel()
            {
                Id = Guid.NewGuid(),
                Codigo = "en-US",
                Nome = "English USA",
                Descricao = "Idioma inglês dos EUA",
                Status = StatusEnum.ATIVO,
                Canal = 1, // App
                EhPadrao = false
            };

            return (testSeed, testSeed2);
        }

        private static void SeedContext(AppDbContext context, IdiomaModel testSeed, IdiomaModel testSeed2)
        {
            context.Idioma.Add(testSeed);
            context.Idioma.Add(testSeed2);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new IdiomaMapper();
            var repository = new IdiomaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetByIdAsync(testSeed.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("pt-BR", resultado.Codigo);
            Assert.Equal("Português Brasil", resultado.Nome);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarNull_QuandoNaoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var mapper = new IdiomaMapper();
            var repository = new IdiomaRepository(context, mapper);

            // Act
            var resultado = await repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task GetByCodigoAsync_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new IdiomaMapper();
            var repository = new IdiomaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetByCodigoAsync("pt-BR", CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Português Brasil", resultado.Nome);
            Assert.True(resultado.EhPadrao);
        }

        [Fact]
        public async Task GetByCodigoAsync_DeveRetornarNull_QuandoNaoEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new IdiomaMapper();
            var repository = new IdiomaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetByCodigoAsync("fr-FR", CancellationToken.None);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task GetPadraoAsync_DeveRetornarIdiomaPadrao()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new IdiomaMapper();
            var repository = new IdiomaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetPadraoAsync(CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.EhPadrao);
            Assert.Equal("pt-BR", resultado.Codigo);
        }

        [Fact]
        public async Task GetByCanalAsync_DeveRetornarIdiomaspelCanal()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new IdiomaMapper();
            var repository = new IdiomaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act - Buscar idiomas do App (canal = 1)
            var resultado = await repository.GetByCanalAsync(1, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Contains(resultado, x => x.Codigo == "en-US");
        }

        [Fact]
        public async Task GetAllAtivosAsync_DeveRetornarApenasAtivos()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            testSeed2.Status = StatusEnum.INATIVO;
            
            using var context = CriarContextoInMemory();
            var mapper = new IdiomaMapper();
            var repository = new IdiomaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetAllAtivosAsync(CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Contains(resultado, x => x.Codigo == "pt-BR");
        }

        [Fact]
        public async Task AddAsync_DeveAdicionar()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var mapper = new IdiomaMapper();
            var repository = new IdiomaRepository(context, mapper);

            var entidade = new Idioma(
                "fr-FR",
                "Francês",
                "Idioma francês",
                StatusEnum.ATIVO,
                Domain.Enums.CanalEnum.Retaguarda,
                false
            );

            // Act
            var resultado = await repository.AddAsync(entidade);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("fr-FR", resultado.Codigo);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizar()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            SeedContext(context, testSeed, testSeed2);
            context.ChangeTracker.Clear();
            
            var mapper = new IdiomaMapper();
            var repository = new IdiomaRepository(context, mapper);

            var toUpdate = await context.Idioma
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == testSeed2.Id);

            if (toUpdate == null)
                return;

            var novoNome = "English United States";
            toUpdate.Nome = novoNome;

            // Act
            var entidade = mapper.ToDomain(toUpdate);
            await repository.UpdateAsync(entidade);

            // Assert
            context.ChangeTracker.Clear();
            var updatedModel = await context.Idioma.FindAsync(testSeed2.Id);

            Assert.NotNull(updatedModel);
            Assert.Equal(novoNome, updatedModel.Nome);
        }

        [Fact]
        public async Task DeleteAsync_DeveDeletar()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            SeedContext(context, testSeed, testSeed2);
            
            var mapper = new IdiomaMapper();
            var repository = new IdiomaRepository(context, mapper);
            var idToDelete = testSeed2.Id;

            // Act
            await repository.DeleteAsync(idToDelete);

            // Assert
            context.ChangeTracker.Clear();
            var deletedModel = await context.Idioma.FindAsync(idToDelete);
            Assert.Null(deletedModel);
        }
    }
}
