using FluentValidation;
using GateAPI.Application.UseCases.Configuracao.MotoristaUC.Atualizar;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Validators.MotoristaValidators;
using Xunit;

namespace GateAPI.Tests.Validators.Configuracao.MotoristaValidators
{
    public class AtualizarMotoristaCommandValidatorTests
    {
        private readonly AtualizarMotoristaCommandValidator _validator;
        private readonly Guid _validId = Guid.NewGuid();

        public AtualizarMotoristaCommandValidatorTests()
        {
            _validator = new AtualizarMotoristaCommandValidator();
        }

        #region ID Tests

        [Fact]
        public void Validate_DeveFalhar_QuandoIdEstaVazio()
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                Guid.Empty,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "12345678901",
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Id" && e.ErrorMessage.Contains("obrigatório"));
        }

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoIdEValido()
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "12345678901",
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        #endregion

        #region Nome Tests

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveFalhar_QuandoNomeEstaVazio(string nome)
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                nome,
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "12345678901",
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Nome" && e.ErrorMessage.Contains("obrigatório"));
        }

        [Fact]
        public void Validate_DeveFalhar_QuandoNomeMenorQueMinimo()
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                "AB",
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "12345678901",
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Nome" && e.ErrorMessage.Contains("mínimo 3 caracteres"));
        }

        [Fact]
        public void Validate_DeveFalhar_QuandoNomeMaiorQueMaximo()
        {
            // Arrange
            var nomeLongo = new string('A', 101);
            var command = new AtualizarMotoristaCommand(
                _validId,
                nomeLongo,
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "12345678901",
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Nome" && e.ErrorMessage.Contains("máximo 100 caracteres"));
        }

        #endregion

        #region CPF Tests

        [Fact]
        public void Validate_DeveFalhar_QuandoCpfEstaVazio()
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "",
                "12345678901",
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "CPF" && e.ErrorMessage.Contains("obrigatório"));
        }

        [Theory]
        [InlineData("000")]
        [InlineData("1234567890")]
        [InlineData("123456789012")]
        public void Validate_DeveFalhar_QuandoCpfNaoPossuiExatamente11Caracteres(string cpf)
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                cpf,
                "12345678901",
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "CPF" && e.ErrorMessage.Contains("exatamente 11 caracteres"));
        }

        #endregion

        #region RG Tests

        [Fact]
        public void Validate_DeveFalhar_QuandoRgEstaVazio()
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "",
                "12345678901",
                "12345678901",
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "RG" && e.ErrorMessage.Contains("obrigatório"));
        }

        [Fact]
        public void Validate_DeveFalhar_QuandoRgMaiorQueMaximo()
        {
            // Arrange
            var rgLongo = new string('A', 21);
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                rgLongo,
                "12345678901",
                "12345678901",
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "RG" && e.ErrorMessage.Contains("máximo 20 caracteres"));
        }

        #endregion

        #region CNH Tests

        [Fact]
        public void Validate_DeveFalhar_QuandoCnhEstaVazia()
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "",
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "CNH" && e.ErrorMessage.Contains("obrigatória"));
        }

        [Theory]
        [InlineData("000")]
        [InlineData("1234567890")]
        [InlineData("123456789012")]
        public void Validate_DeveFalhar_QuandoCnhNaoPossuiExatamente11Caracteres(string cnh)
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                cnh,
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "CNH" && e.ErrorMessage.Contains("exatamente 11 caracteres"));
        }

        #endregion

        #region Telefone Tests

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoTelefoneEhNulo()
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "12345678901",
                new DateOnly(2026, 12, 31),
                null,
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_DeveFalhar_QuandoTelefoneEMuitoLongo()
        {
            // Arrange
            var telefoneLongo = new string('9', 21);
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "12345678901",
                new DateOnly(2026, 12, 31),
                telefoneLongo,
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Telefone" && e.ErrorMessage.Contains("máximo 20 caracteres"));
        }

        [Theory]
        [InlineData("319999999")]
        [InlineData("3199999999999")]
        public void Validate_DeveFalhar_QuandoTelefoneNaoMatcheaPadrao(string telefone)
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "12345678901",
                new DateOnly(2026, 12, 31),
                telefone,
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Telefone" && e.ErrorMessage.Contains("entre 10 e 11 dígitos"));
        }

        #endregion

        #region Foto Tests

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoFotoEhNula()
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "12345678901",
                new DateOnly(2026, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_DeveFalhar_QuandoFotoEMuitoLonga()
        {
            // Arrange
            var fotoLonga = "https://" + new string('A', 494);
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "12345678901",
                new DateOnly(2026, 12, 31),
                "31999999999",
                fotoLonga,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Foto" && e.ErrorMessage.Contains("máximo 500 caracteres"));
        }

        #endregion

        #region ValidadeCnh Tests

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoValidadeCnhEhNula()
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "12345678901",
                null,
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_DeveFalhar_QuandoValidadeCnhEstaNoPassado()
        {
            // Arrange
            var command = new AtualizarMotoristaCommand(
                _validId,
                "João da Silva",
                new DateOnly(1985, 5, 20),
                "123456789",
                "12345678901",
                "12345678901",
                new DateOnly(2020, 12, 31),
                "31999999999",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "ValidadeCnh" && e.ErrorMessage.Contains("passado"));
        }

        #endregion
    }
}