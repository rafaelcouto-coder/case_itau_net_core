using CaseItau.Application.Abstractions.Messaging;

namespace CaseItau.Application.Fundos.DeleteFundos;

public sealed record DeleteFundosCommand(
    string Codigo
 ) : ICommand<string>;