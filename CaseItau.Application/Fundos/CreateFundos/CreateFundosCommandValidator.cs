using FluentValidation;

namespace CaseItau.Application.Fundos.CreateFundos;

public sealed class CreateFundosCommandValidator : AbstractValidator<CreateFundosCommand>
{
    public CreateFundosCommandValidator()
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

        RuleFor(x => x.Patrimonio)
            .GreaterThanOrEqualTo(0).WithMessage("The Patrimonio must be greater than or equal to 0.");
    }
}
