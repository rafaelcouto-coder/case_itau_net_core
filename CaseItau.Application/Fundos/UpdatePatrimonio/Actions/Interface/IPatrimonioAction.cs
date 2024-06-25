using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;

namespace CaseItau.Application.Fundos.UpdatePatrimonio.Actions.Interface;

public interface IPatrimonioAction
{
    Result<string> Execute(Fundo fundo, decimal patrimonio);
}
