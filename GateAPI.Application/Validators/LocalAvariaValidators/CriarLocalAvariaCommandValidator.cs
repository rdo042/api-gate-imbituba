using FluentValidation;
using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Criar;

namespace GateAPI.Application.Validators.LocalAvariaValidators
{
    /// <summary>
    /// Validador FluentValidation para CriarLocalAvariaCommand
    /// </summary>
    public class CriarLocalAvariaCommandValidator : AbstractValidator<CriarLocalAvariaCommand>
    {
        public CriarLocalAvariaCommandValidator()
        {
            RuleFor(x => x.Local)
                .NotEmpty()
                .WithMessage("Local de avaria é obrigatório")
                .MinimumLength(3)
                .WithMessage("Local deve ter no mínimo 3 caracteres")
                .MaximumLength(100)
                .WithMessage("Local deve ter no máximo 100 caracteres");

            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Descrição é obrigatória")
                .MaximumLength(255)
                .WithMessage("Descrição deve ter no máximo 255 caracteres");
        }
    }
}
