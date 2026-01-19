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
    public class PerfilRepositoryTests
    {
        private static AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var accessor = new HttpContextAccessor();
            return new AppDbContext(options, accessor);
        }

        private static readonly PerfilModel _testSeed = new()
        {
            Id = Guid.NewGuid(),
            Nome = "ADM",
            Status = StatusEnum.ATIVO,
            Permissoes =
            [
                new() {
                    Id = Guid.NewGuid(),
                    Nome = "PERM001"
                }
            ]
        };

        private static void SeedContext(AppDbContext context)
        {
            context.Perfil.Add(_testSeed);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetById_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new PerfilRepository(context);

            SeedContext(context);

            // Act
            var resultado = await repository.GetByIdAsync(_testSeed.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("ADM", resultado.Nome);
            Assert.Single(resultado.Permissoes);
        }

        [Fact]
        public async Task GetAll_DeveRetornarLista()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new PerfilRepository(context);

            SeedContext(context);

            // Act
            var resultado = await repository.GetAllAsync();

            // Assert
            Assert.Single(resultado);
        }

        [Fact]
        public async Task AddAsync_DeveAdicionar()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new PerfilRepository(context);

            var entidade = new Perfil(
                "ADM",
                null,
                []
                );

            // Act
            var resultado = await repository.AddAsync(entidade);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("ADM", resultado.Nome);
        }

        [Fact]
        public async Task UpdateAsync_DevePerfilUsuario_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            SeedContext(context);
            context.ChangeTracker.Clear();
            var repository = new PerfilRepository(context);

            var toUpdate = await context.Perfil
                                .AsNoTracking()
                                .Include(x => x.Permissoes)
                                .FirstOrDefaultAsync(x => x.Id == _testSeed.Id);

            if (toUpdate == null)
                return;

            var newNome = "OPR";
            toUpdate.Nome = newNome;

            PermissaoModel newPermissao = new()
            {
                Id = Guid.NewGuid(),
                Nome = "PERM002"
            };

            context.Permissao.Add(newPermissao);
            context.SaveChanges();

            toUpdate.Permissoes = [ newPermissao ];

            // Act
            var entidade = PerfilMapper.ToDomain(toUpdate);
            await repository.UpdateAsync(entidade);

            // Assert
            context.ChangeTracker.Clear();
            var updatedModel = await context.Perfil
                                .AsNoTracking()
                                .Include(x => x.Permissoes)
                                .FirstOrDefaultAsync(x => x.Id == _testSeed.Id);

            Assert.NotNull(updatedModel);
            Assert.Equal(newNome, updatedModel.Nome);
            Assert.Equal("PERM002", updatedModel.Permissoes?.FirstOrDefault()?.Nome);
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverUsuario_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            SeedContext(context);
            var repository = new PerfilRepository(context);
            var idToDelete = _testSeed.Id;

            // Act
            await repository.DeleteAsync(idToDelete);

            // Assert
            var deletedModel = await context.Permissao.FindAsync(idToDelete);
            Assert.Null(deletedModel);
        }
    }
}
