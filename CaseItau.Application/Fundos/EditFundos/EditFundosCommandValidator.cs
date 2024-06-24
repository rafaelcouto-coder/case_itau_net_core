using FluentValidation;

namespace CaseItau.Application.Fundos.EditFundos;

public sealed class EditFundosCommandValidator : AbstractValidator<EditFundosCommand>
{
    public EditFundosCommandValidator()
    {
        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("The Codigo is required.");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("The Nome is required.");

        RuleFor(x => x.Cnpj)
            .NotEmpty().WithMessage("The CNPJ is required.")
            .Length(14).WithMessage("The CNPJ must contain 14 digits.");

        RuleFor(x => x.TipoFundo)
            .IsInEnum().WithMessage("The TipoFundo is invalid.")
            .NotEmpty().WithMessage("The TipoFundo is required.");
    }
}