using CaseItau.Application.Fundos.UpdatePatrimonio.Actions;
using CaseItau.Application.Fundos.UpdatePatrimonio.Actions.Interface;
using CaseItau.Domain.Fundos.Enums;

namespace CaseItau.Application.Fundos.UpdatePatrimonio.Factories;

internal sealed class PatrimonioActionFactory
{
    public static IPatrimonioAction GetAction(PatrimonyOperation operation)
    {
        return operation switch
        {
            PatrimonyOperation.Add => new AddPatrimonioAction(),
            PatrimonyOperation.Subtract => new RemovePatrimonioAction(),
            _ => throw new ArgumentException("Invalid operation", nameof(operation)),
        };
    }
}
