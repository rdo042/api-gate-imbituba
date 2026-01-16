using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Infra.Mappers.Configuracao;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using GateAPI.Infra.Persistence.Repositories.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Tests.Repositories.Configuracao
{
    public class TipoLacreRepositoryTests
    {
        private static AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        private static readonly TipoLacreModel _testSeed = new()
        {
            Id = Guid.NewGuid(),
            Tipo = "LAC001",
            Descricao = "",
            Status = StatusEnum.ATIVO
        };
        private static readonly TipoLacreModel _testSeed2 = new()
        {
            Id = Guid.NewGuid(),
            Tipo = "LAC002",
            Descricao = "",
            Status = StatusEnum.ATIVO
        };

        private static void SeedContext(AppDbContext context)
        {
            context.TipoLacre.Add(_testSeed);
            context.TipoLacre.Add(_testSeed2);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetById_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new TipoLacreRepository(context);

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
            var repository = new TipoLacreRepository(context);

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
            var repository = new TipoLacreRepository(context);

            var entidade = new TipoLacre(
                "LAC001",
                "",
                StatusEnum.ATIVO
                );

            // Act
            var resultado = await repository.AddAsync(entidade);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("LAC001", resultado.Tipo);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarTipoLacre_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            SeedContext(context);
            context.ChangeTracker.Clear();
            var repository = new TipoLacreRepository(context);

            var toUpdate = await context.TipoLacre
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == _testSeed2.Id);

            if (toUpdate == null)
                return;

            var newDesc = "Teste";
            toUpdate.Descricao = newDesc;

            // Act
            var entidade = TipoLacreMapper.ToDomain(toUpdate);
            await repository.UpdateAsync(entidade);

            // Assert
            context.ChangeTracker.Clear();
            var updatedModel = await context.TipoLacre.FindAsync(_testSeed2.Id);

            Assert.NotNull(updatedModel);
            Assert.Equal(newDesc, updatedModel.Descricao);
            Assert.Equal(_testSeed2.Id, updatedModel.Id);
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverTipoLacre_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            SeedContext(context);
            var repository = new TipoLacreRepository(context);
            var idToDelete = _testSeed.Id;

            // Act
            await repository.DeleteAsync(idToDelete);

            // Assert
            var deletedModel = await context.TipoLacre.FindAsync(idToDelete);
            Assert.Null(deletedModel);
        }
    }
}
