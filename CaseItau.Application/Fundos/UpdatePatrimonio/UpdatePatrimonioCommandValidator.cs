using FluentValidation;

namespace CaseItau.Application.Fundos.UpdatePatrimonio;

public sealed class UpdatePatrimonioCommandValidator : AbstractValidator<UpdatePatrimonioCommand>
{
    public UpdatePatrimonioCommandValidator()
    {
        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("The Codigo is required.");

        RuleFor(x => x.Patrimonio)
            .GreaterThanOrEqualTo(0).WithMessage("The value must be greater than or equal to 0.");

        RuleFor(x => x.Operation)
            .IsInEnum().WithMessage("The Operation is invalid.");
    }
}
