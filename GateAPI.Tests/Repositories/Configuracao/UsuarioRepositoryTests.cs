using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Infra.Mappers.Configuracao;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using GateAPI.Infra.Persistence.Repositories.Configuracao;
using GateAPI.Infra.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Tests.Repositories.Configuracao
{
    public class UsuarioRepositoryTests
    {
        private static AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var accessor = new HttpContextAccessor();
            return new AppDbContext(options, accessor);
        }

        private static readonly PasswordHasher _hasher = new();

        private static readonly UsuarioModel _testSeed = new()
        {
            Id = Guid.NewGuid(),
            Nome = "John Doe",
            Email = "johndoe@email.com",
            SenhaHash = _hasher.HashPassword("#Senha123"),
            Status = StatusEnum.ATIVO
        };

        private static readonly UsuarioModel _testSeed2 = new()
        {
            Id = Guid.NewGuid(),
            Nome = "Jane Doe",
            Email = "janedoe@email.com",
            SenhaHash = _hasher.HashPassword("#Senha123"),
            Status = StatusEnum.ATIVO
        };

        private static void SeedContext(AppDbContext context)
        {
            context.Usuario.Add(_testSeed);
            context.Usuario.Add(_testSeed2);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetById_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new UsuarioRepository(context);

            SeedContext(context);

            // Act
            var resultado = await repository.GetByIdAsync(_testSeed.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("John Doe", resultado.Nome);
            Assert.Equal("johndoe@email.com", resultado.Email);
        }

        [Fact]
        public async Task GetById_DeveRetornarNull_QuandoNaoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new UsuarioRepository(context);

            // Act
            var resultado = await repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task GetByEmail_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new UsuarioRepository(context);

            SeedContext(context);

            // Act
            var resultado = await repository.GetByEmailAsync(_testSeed.Email);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("John Doe", resultado.Nome);
            Assert.Equal("johndoe@email.com", resultado.Email);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornar()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new UsuarioRepository(context);

            SeedContext(context);

            // Act
            var resultado = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task GetAllPaginatedAsync_DeveRetornar()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new UsuarioRepository(context);

            SeedContext(context);

            int page = 1;
            int pageSize = 1;
            string sortDirection = "asc";

            // Act
            var resultado = await repository.GetAllPaginatedAsync(page, pageSize, null, sortDirection, null);

            // Assert
            Assert.Equal(2, resultado.Item2);
            Assert.Single(resultado.Item1);
        }

        [Fact]
        public async Task AddAsync_DeveAdicionar()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var repository = new UsuarioRepository(context);

            var entidade = new Usuario(
                "Jane Doe",
                "jane@doe.com",
                _hasher.HashPassword("#Senha123"),
                null,
                null
                );

            // Act
            var resultado = await repository.AddAsync(entidade);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Jane Doe", resultado.Nome);
            Assert.Equal("jane@doe.com", resultado.Email);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarUsuario_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            SeedContext(context);
            context.ChangeTracker.Clear();
            var repository = new UsuarioRepository(context);

            var toUpdate = await context.Usuario
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == _testSeed.Id);

            if (toUpdate == null)
                return;

            var newNome = "Jane Doe";
            toUpdate.Nome = newNome;

            // Act
            var entidade = UsuarioMapper.ToDomain(toUpdate);
            await repository.UpdateAsync(entidade);

            // Assert
            context.ChangeTracker.Clear();
            var updatedModel = await context.Usuario.FindAsync(_testSeed.Id);

            Assert.NotNull(updatedModel);
            Assert.Equal(newNome, updatedModel.Nome);
            Assert.Equal(_testSeed.Id, updatedModel.Id);
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverSuaveUsuario_QuandoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            SeedContext(context);
            var repository = new UsuarioRepository(context);
            var idToDelete = _testSeed.Id;

            // Act
            await repository.DeleteAsync(idToDelete);

            // Assert
            context.ChangeTracker.Clear();
            var notFoundDeleted = await context.Usuario.FindAsync(idToDelete);
            Assert.Null(notFoundDeleted);

            var deletedModel = await context.Usuario.IgnoreQueryFilters().FirstAsync(x=> x.Id == idToDelete);
            Assert.NotNull(deletedModel);
            Assert.False(deletedModel.DeletedAt == null);

        }
    }
}
