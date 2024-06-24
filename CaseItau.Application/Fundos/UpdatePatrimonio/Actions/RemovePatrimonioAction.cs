using CaseItau.Application.Fundos.UpdatePatrimonio.Actions.Interface;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;

namespace CaseItau.Application.Fundos.UpdatePatrimonio.Actions;

internal class RemovePatrimonioAction : IPatrimonioAction
{
    public Result<string> Execute(Fundo fundo, decimal patrimonio)
    {
        fundo.Patrimonio ??= 0;

        if (fundo.Patrimonio - patrimonio < 0)
            return Result.Failure<string>(FundoErrors.NegativePatrimonyNotAllowed);

        fundo.Patrimonio -= patrimonio;
        return Result.Success(fundo.Codigo);
    }
}
