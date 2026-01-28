using FluentValidation;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar;

namespace GateAPI.Domain.Validators.TipoAvariaValidators
{
    /// <summary>
    /// Validador FluentValidation para CriarTipoAvariaCommand
    /// </summary>
    public class CriarTipoAvariaCommandValidator : AbstractValidator<CriarTipoAvariaCommand>
    {
        public CriarTipoAvariaCommandValidator()
        {
            RuleFor(x => x.Tipo)
                .NotEmpty()
                .WithMessage("Tipo de avaria é obrigatório")
                .MinimumLength(3)
                .WithMessage("Tipo deve ter no mínimo 3 caracteres")
                .MaximumLength(50)
                .WithMessage("Tipo deve ter no máximo 50 caracteres");

            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Descrição é obrigatória")
                .MaximumLength(300)
                .WithMessage("Descrição deve ter no máximo 300 caracteres");
        }
    }
}
