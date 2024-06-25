using CaseItau.Application.Fundos.UpdatePatrimonio.Actions.Interface;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;

namespace CaseItau.Application.Fundos.UpdatePatrimonio.Actions;

internal sealed class AddPatrimonioAction : IPatrimonioAction
{
    public Result<string> Execute(Fundo fundo, decimal patrimonio)
    {
        fundo.Patrimonio = (fundo.Patrimonio ?? 0) + patrimonio;
        return Result.Success(fundo.Codigo);
    }
}
