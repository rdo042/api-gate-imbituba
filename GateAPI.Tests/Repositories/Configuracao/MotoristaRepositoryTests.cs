using GateAPI.Domain.Enums;
using GateAPI.Infra.Mappers.Configuracao;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using GateAPI.Infra.Persistence.Repositories.Configuracao;
using GateAPI.Tests.Entities.Configuracao.Stubs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Tests.Repositories.Configuracao
{
    public class MotoristaRepositoryTests
    {
        private static AppDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var accessor = new HttpContextAccessor();
            return new AppDbContext(options, accessor);
        }

        private static (MotoristaModel, MotoristaModel) CriarSeedPadrao()
        {
            var stub = MotoristaStub.Valid();
            var stub2 = MotoristaStub.Valid01();
            
    var testSeed = new MotoristaModel()
            {
                Id = Guid.NewGuid(),
                Nome = "João da Silva",
                DataNascimento = new DateOnly(1985, 5, 20),
                ValidadeCNH = new DateOnly(2026, 12, 31),
                Telefone = "31999999999",
                RG = stub.RG.Value,
                CPF = stub.CPF.Value,
                CNH = stub.CNH.Value,
                Foto = "https://example.com/foto1.jpg",
                Status = StatusEnum.ATIVO
            };

            var testSeed2 = new MotoristaModel()
            {
                Id = Guid.NewGuid(),
                Nome = "Jane Silva",
                DataNascimento = new DateOnly(1990, 3, 15),
                RG = stub2.RG.Value,
                CPF = stub2.CPF.Value,
                CNH = stub2.CNH.Value,
                ValidadeCNH = new DateOnly(2027, 6, 30),
                Telefone = "31988888888",
                Foto = "https://example.com/foto2.jpg",
                Status = StatusEnum.ATIVO
            };

            return (testSeed, testSeed2);
        }

        private static void SeedContext(AppDbContext context, MotoristaModel testSeed, MotoristaModel testSeed2)
        {
            context.Motorista.Add(testSeed);
            context.Motorista.Add(testSeed2);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornar_QuandoEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetByIdAsync(testSeed.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("João da Silva", resultado.Nome);
            Assert.Equal(testSeed.Id, resultado.Id);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarNull_QuandoNaoEncontrado()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

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
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            var motorista = MotoristaStub.Valid();

            // Act
            var resultado = await repository.AddAsync(motorista);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("João da Silva", resultado.Nome);
            Assert.NotEqual(Guid.Empty, resultado.Id);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarMotorista_QuandoEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            SeedContext(context, testSeed, testSeed2);
            context.ChangeTracker.Clear();

            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            var motorista = mapper.ToDomain(testSeed2);
            var novoNome = "Maria Silva Updated";
            motorista.UpdateEntity(novoNome);

            // Act
            await repository.UpdateAsync(motorista);

            // Assert
            context.ChangeTracker.Clear();
            var updatedModel = await context.Motorista.FindAsync(testSeed2.Id);

            Assert.NotNull(updatedModel);
            Assert.Equal(novoNome, updatedModel.Nome);
            Assert.Equal(testSeed2.Id, updatedModel.Id);
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverMotorista_QuandoEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            SeedContext(context, testSeed, testSeed2);
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);
            var idToDelete = testSeed.Id;

            // Act
            var resultado = await repository.DeleteAsync(idToDelete);

            // Assert
            Assert.True(resultado);
            context.ChangeTracker.Clear();
            var notFoundDeleted = await context.Motorista.FindAsync(idToDelete);
            Assert.Null(notFoundDeleted);

            var deletedModel = await context.Motorista.IgnoreQueryFilters().FirstAsync(x => x.Id == idToDelete);
            Assert.NotNull(deletedModel);
            Assert.NotNull(deletedModel.DeletedAt);
        }

        [Fact]
        public async Task DeleteAsync_DeveRetornarFalse_QuandoMotoristaNoaoExiste()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            // Act
            var resultado = await repository.DeleteAsync(Guid.NewGuid());

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task GetByDocumentoAsync_DeveRetornar_QuandoCPFEncontrado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetByDocumentoAsync(testSeed.CPF);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("João da Silva", resultado.Nome);
            Assert.Equal(testSeed.CPF, resultado.CPF.Value);
        }

        [Fact]
        public async Task GetByDocumentoAsync_DeveRetornar_QuandoCNHEncontrada()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetByDocumentoAsync(testSeed2.CNH);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Jane Silva", resultado.Nome);
            Assert.Equal(testSeed2.CNH, resultado.CNH.Value);
        }

        [Fact]
        public async Task GetByDocumentoAsync_DeveRetornarNull_QuandoDocumentoNoaoExiste()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetByDocumentoAsync("00000000000");

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task GetByDocumentoAsync_DeveRetornarNull_QuandoMotoristaFoiDeletado()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            await repository.DeleteAsync(testSeed.Id);

            // Assert
            context.ChangeTracker.Clear();
            var resultado = await repository.GetByDocumentoAsync(testSeed.CPF);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarTodosMotoristas()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var resultado = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Contains(resultado, x => x.Nome == "João da Silva");
            Assert.Contains(resultado, x => x.Nome == "Jane Silva");
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarListaVazia_QuandoNenhumMotoristaExiste()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            // Act
            var resultado = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Empty(resultado);
        }

        [Fact]
        public async Task GetAllPaginatedAsync_DeveRetornarPaginado_QuandoMotoristaExistem()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            var (items, totalCount) = await repository.GetAllPaginatedAsync(pageNumber: 1, pageSize: 10);

            // Assert
            Assert.NotNull(items);
            Assert.Equal(2, items.Count());
            Assert.Equal(2, totalCount);
            Assert.Contains(items, x => x.Nome == "João da Silva");
        }

        [Fact]
        public async Task GetAllPaginatedAsync_DeveRetornarListaVazia_QuandoNenhumMotoristaExiste()
        {
            // Arrange
            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            // Act
            var (items, totalCount) = await repository.GetAllPaginatedAsync(pageNumber: 1, pageSize: 10);

            // Assert
            Assert.NotNull(items);
            Assert.Empty(items);
            Assert.Equal(0, totalCount);
        }

        [Fact]
        public async Task GetAllPaginatedAsync_DeveRespeitar_PaginaEAl()
        {
            // Arrange
            var motorista1 = new MotoristaModel
            {
                Id = Guid.NewGuid(),
                Nome = "Motorista 1",
                DataNascimento = new DateOnly(1985, 5, 20),
                RG = "111111111",
                CPF = "11111111111",
                CNH = "11111111100",
                ValidadeCNH = new DateOnly(2026, 12, 31),
                Telefone = "31911111111",
                Foto = "",
                Status = StatusEnum.ATIVO
            };

            var motorista2 = new MotoristaModel
            {
                Id = Guid.NewGuid(),
                Nome = "Motorista 2",
                DataNascimento = new DateOnly(1986, 6, 21),
                RG = "222222222",
                CPF = "22222222222",
                CNH = "22222222200",
                ValidadeCNH = new DateOnly(2027, 1, 15),
                Telefone = "31922222222",
                Foto = "",
                Status = StatusEnum.ATIVO
            };

            var motorista3 = new MotoristaModel
            {
                Id = Guid.NewGuid(),
                Nome = "Motorista 3",
                DataNascimento = new DateOnly(1987, 7, 22),
                RG = "333333333",
                CPF = "33333333333",
                CNH = "33333333300",
                ValidadeCNH = new DateOnly(2028, 2, 28),
                Telefone = "31933333333",
                Foto = "",
                Status = StatusEnum.ATIVO
            };

            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            context.Motorista.AddRange(motorista1, motorista2, motorista3);
            context.SaveChanges();

            // Act
            var (items, totalCount) = await repository.GetAllPaginatedAsync(pageNumber: 2, pageSize: 1);

            // Assert
            Assert.NotNull(items);
            Assert.Single(items);
            Assert.Equal(3, totalCount);
            Assert.Equal("Motorista 2", items.First().Nome);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarApenasNaoDeletados()
        {
            // Arrange
            var (testSeed, testSeed2) = CriarSeedPadrao();
            using var context = CriarContextoInMemory();
            var mapper = new MotoristaMapper();
            var repository = new MotoristaRepository(context, mapper);

            SeedContext(context, testSeed, testSeed2);

            // Act
            await repository.DeleteAsync(testSeed.Id);
            context.ChangeTracker.Clear();

            var resultado = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal("Jane Silva", resultado.First().Nome);
        }
    }
}