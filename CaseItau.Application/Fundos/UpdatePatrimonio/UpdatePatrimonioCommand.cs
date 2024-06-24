using CaseItau.Application.Abstractions.Messaging;
using CaseItau.Domain.Fundos.Enums;

namespace CaseItau.Application.Fundos.UpdatePatrimonioCommand;

public sealed record UpdatePatrimonioCommand(
    string Codigo,
    decimal Patrimonio,
    PatrimonyOperation Operation
) : ICommand<string>;