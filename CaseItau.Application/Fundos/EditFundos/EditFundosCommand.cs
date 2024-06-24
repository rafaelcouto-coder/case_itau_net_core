using CaseItau.Application.Abstractions.Messaging;

namespace CaseItau.Application.Fundos.EditFundos;

public sealed record EditFundosCommand(
    string Codigo,
    string Nome,
    string Cnpj,
    int TipoFundo
 ) : ICommand<string>;
