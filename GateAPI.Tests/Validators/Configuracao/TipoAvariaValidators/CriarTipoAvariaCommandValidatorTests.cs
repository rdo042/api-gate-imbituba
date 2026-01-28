using FluentValidation;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Validators.TipoAvariaValidators;
using Xunit;

namespace GateAPI.Tests.Validators.Configuracao.TipoAvariaValidators
{
    public class CriarTipoAvariaCommandValidatorTests
    {
        private readonly CriarTipoAvariaCommandValidator _validator;

        public CriarTipoAvariaCommandValidatorTests()
        {
            _validator = new CriarTipoAvariaCommandValidator();
        }

        #region Tipo Tests

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoTipoEValido()
        {
            // Arrange
            var command = new CriarTipoAvariaCommand(
                "Motor Queimado",
                "Avaria relacionada ao motor",
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveFalhar_QuandoTipoEstaVazio(string tipo)
        {
            // Arrange
            var command = new CriarTipoAvariaCommand(
                tipo,
                "Descrição válida",
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Tipo" && e.ErrorMessage.Contains("obrigatório"));
        }

        [Fact]
        public void Validate_DeveFalhar_QuandoTipoMenorQueMinimo()
        {
            // Arrange
            var command = new CriarTipoAvariaCommand(
                "AB",
                "Descrição válida",
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Tipo" && e.ErrorMessage.Contains("mínimo 3 caracteres"));
        }

        [Fact]
        public void Validate_DeveFalhar_QuandoTipoMaiorQueMaximo()
        {
            // Arrange
            var tipoLongo = new string('A', 51);
            var command = new CriarTipoAvariaCommand(
                tipoLongo,
                "Descrição válida",
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Tipo" && e.ErrorMessage.Contains("máximo 50 caracteres"));
        }

        #endregion

        #region Descricao Tests

        [Fact]
        public void Validate_DeveFalhar_QuandoDescricaoEstaVazia()
        {
            // Arrange
            var command = new CriarTipoAvariaCommand(
                "Motor Queimado",
                "",
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Descricao" && e.ErrorMessage.Contains("obrigatória"));
        }

        [Theory]
        [InlineData("   ")]
        public void Validate_DeveFalhar_QuandoDescricaoEstaEmBranco(string descricao)
        {
            // Arrange
            var command = new CriarTipoAvariaCommand(
                "Motor Queimado",
                descricao,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Descricao" && e.ErrorMessage.Contains("obrigatória"));
        }

        [Fact]
        public void Validate_DeveFalhar_QuandoDescricaoMaiorQueMaximo()
        {
            // Arrange
            var descricaoLonga = new string('A', 301);
            var command = new CriarTipoAvariaCommand(
                "Motor Queimado",
                descricaoLonga,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Descricao" && e.ErrorMessage.Contains("máximo 300 caracteres"));
        }

        #endregion
    }
}